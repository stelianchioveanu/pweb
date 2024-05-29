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

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface FeedbackAddDTO
 */
export interface FeedbackAddDTO {
    /**
     * 
     * @type {string}
     * @memberof FeedbackAddDTO
     */
    description?: string | null;
    /**
     * 
     * @type {number}
     * @memberof FeedbackAddDTO
     */
    stars?: number;
}

/**
 * Check if a given object implements the FeedbackAddDTO interface.
 */
export function instanceOfFeedbackAddDTO(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function FeedbackAddDTOFromJSON(json: any): FeedbackAddDTO {
    return FeedbackAddDTOFromJSONTyped(json, false);
}

export function FeedbackAddDTOFromJSONTyped(json: any, ignoreDiscriminator: boolean): FeedbackAddDTO {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'description': !exists(json, 'description') ? undefined : json['description'],
        'stars': !exists(json, 'stars') ? undefined : json['stars'],
    };
}

export function FeedbackAddDTOToJSON(value?: FeedbackAddDTO | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'description': value.description,
        'stars': value.stars,
    };
}
