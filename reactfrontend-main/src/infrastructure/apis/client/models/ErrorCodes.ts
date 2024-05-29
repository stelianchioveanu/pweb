/* tslint:disable */
/* eslint-disable */
/**
 * MobyLab Web App
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


/**
 * 
 * @export
 */
export const ErrorCodes = {
    Unknown: 'Unknown',
    TechnicalError: 'TechnicalError',
    EntityNotFound: 'EntityNotFound',
    PhysicalFileNotFound: 'PhysicalFileNotFound',
    UserAlreadyExists: 'UserAlreadyExists',
    WrongPassword: 'WrongPassword',
    CannotAdd: 'CannotAdd',
    CannotUpdate: 'CannotUpdate',
    CannotDelete: 'CannotDelete',
    MailSendFailed: 'MailSendFailed',
    TagAlreadyExists: 'TagAlreadyExists',
    WrongTag: 'WrongTag',
    WrongInputs: 'WrongInputs',
    OrderAlreadyExists: 'OrderAlreadyExists'
} as const;
export type ErrorCodes = typeof ErrorCodes[keyof typeof ErrorCodes];


export function ErrorCodesFromJSON(json: any): ErrorCodes {
    return ErrorCodesFromJSONTyped(json, false);
}

export function ErrorCodesFromJSONTyped(json: any, ignoreDiscriminator: boolean): ErrorCodes {
    return json as ErrorCodes;
}

export function ErrorCodesToJSON(value?: ErrorCodes | null): any {
    return value as any;
}

