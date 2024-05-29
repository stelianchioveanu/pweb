import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormWatch
} from "react-hook-form";
import { SelectChangeEvent } from "@mui/material";

export type TagAddFormModel = {
    tag: string;
};

export type TagAddFormState = {
    errors: FieldErrorsImpl<DeepRequired<TagAddFormModel>>;
};

export type TagAddFormActions = {
    register: UseFormRegister<TagAddFormModel>;
    watch: UseFormWatch<TagAddFormModel>;
    handleSubmit: UseFormHandleSubmit<TagAddFormModel>;
    submit: (body: TagAddFormModel) => void;
};
export type TagAddFormComputed = {
    defaultValues: TagAddFormModel,
    isSubmitting: boolean
};

export type TagAddFormController = FormController<TagAddFormState, TagAddFormActions, TagAddFormComputed>;