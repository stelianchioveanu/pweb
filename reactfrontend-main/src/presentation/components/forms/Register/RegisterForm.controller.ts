import { RegisterFormController, RegisterFormModel } from "./RegisterForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { isUndefined } from "lodash";
import { useForm } from "react-hook-form";
import { useMutation } from "@tanstack/react-query";
import { useRegisterApi } from "@infrastructure/apis/api-management";
import { useCallback } from "react";
import { useAppRouter } from "@infrastructure/hooks/useAppRouter";
import { toast } from "react-toastify";

/**
 * Use a function to return the default values of the form and the validation schema.
 * You can add other values as the default, for example when populating the form with data to update an entity in the backend.
 */
const getDefaultValues = (initialData?: { email: string }) => {
    const defaultValues = {
        email: "",
        password: "",
        name: "",
        confPass: ""
    };

    if (!isUndefined(initialData)) {
        return {
            ...defaultValues,
            ...initialData,
        };
    }

    return defaultValues;
};

/**
 * Create a hook to get the validation schema.
 */
const useInitRegisterForm = () => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues();

    const schema = yup.object().shape({
        email: yup.string() // Acest câmp ar trebui să fie un string.
            .required(formatMessage( // Folosește formatMessage pentru a obține mesajul de eroare tradus.
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({ // Formatează mesajul cu alte stringuri traduse.
                        id: "globals.email",
                    }),
                })) // Câmpul este obligatoriu și are nevoie de un mesaj de eroare când este gol.
            .email() // Acest câmp trebuie să aibă format de email.
            .default(defaultValues.email), // Adaugă o valoare implicită pentru câmp.
        name: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.name",
                    }),
                }))
            .default(defaultValues.name),
        password: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.password",
                    }),
                }))
            .default(defaultValues.password),
        confPass: yup.string()
            .oneOf([yup.ref('password'), undefined], formatMessage(
                { id: "globals.validations.passwordsMustMatch" }
            ))
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.confPass",
                    }),
                }))
            .default(defaultValues.confPass),
    });

    const resolver = yupResolver(schema); // Get the resolver.

    return { defaultValues, resolver }; // Return the default values and the resolver.
}

/**
 * Create a controller hook for the form and return any data that is necessary for the form.
 */
export const useRegisterFormController = (): RegisterFormController => {
    const { formatMessage } = useIntl();
    const { defaultValues, resolver } = useInitRegisterForm();
    const { redirectToLogin } = useAppRouter();
    const { registerMutation: { mutation, key: mutationKey } } = useRegisterApi();
    const { mutateAsync: registerMut, status } = useMutation({
        mutationKey: [mutationKey],
        mutationFn: mutation
    })
    const submit = useCallback((data: RegisterFormModel) => // Create a submit callback to send the form data to the backend.
        registerMut(data).then((result) => {
            toast(formatMessage({ id: "notifications.messages.registerSuccess" }));
            redirectToLogin();
        }), [registerMut, redirectToLogin]);

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<RegisterFormModel>({ // Use the useForm hook to get callbacks and variables to work with the form.
        defaultValues, // Initialize the form with the default values.
        resolver // Add the validation resolver.
    });

    return {
        actions: { // Return any callbacks needed to interact with the form.
            handleSubmit, // Add the form submit handle.
            submit, // Add the submit handle that needs to be passed to the submit handle.
            register // Add the variable register to bind the form fields in the UI with the form variables.
        },
        computed: {
            defaultValues,
            isSubmitting: status === "pending" // Return if the form is still submitting or nit.
        },
        state: {
            errors // Return what errors have occurred when validating the form input.
        }
    }
}