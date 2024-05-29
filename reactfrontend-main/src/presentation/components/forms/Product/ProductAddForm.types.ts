import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormWatch
} from "react-hook-form";
import { SelectChangeEvent } from "@mui/material";
import { UserRoleEnum } from "@infrastructure/apis/client";

export type ProductAddFormModel = {
    name: string;
    description: string;
    price: number;
    tags: UserRoleEnum[]
};

export type ProductAddFormState = {
    errors: FieldErrorsImpl<DeepRequired<ProductAddFormModel>>;
};

export type ProductAddFormActions = {
    register: UseFormRegister<ProductAddFormModel>;
    watch: UseFormWatch<ProductAddFormModel>;
    handleSubmit: UseFormHandleSubmit<ProductAddFormModel>;
    submit: (body: ProductAddFormModel) => void;
    selectTags: (value: SelectChangeEvent<UserRoleEnum[]>) => void;
};
export type ProductAddFormComputed = {
    defaultValues: ProductAddFormModel,
    isSubmitting: boolean
};

export type ProductAddFormController = FormController<ProductAddFormState, ProductAddFormActions, ProductAddFormComputed>;