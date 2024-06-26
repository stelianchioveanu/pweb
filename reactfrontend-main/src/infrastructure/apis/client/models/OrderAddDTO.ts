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
 * @interface OrderAddDTO
 */
export interface OrderAddDTO {
    /**
     * 
     * @type {string}
     * @memberof OrderAddDTO
     */
    productId?: string;
}

/**
 * Check if a given object implements the OrderAddDTO interface.
 */
export function instanceOfOrderAddDTO(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function OrderAddDTOFromJSON(json: any): OrderAddDTO {
    return OrderAddDTOFromJSONTyped(json, false);
}

export function OrderAddDTOFromJSONTyped(json: any, ignoreDiscriminator: boolean): OrderAddDTO {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'productId': !exists(json, 'productId') ? undefined : json['productId'],
    };
}

export function OrderAddDTOToJSON(value?: OrderAddDTO | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'productId': value.productId,
    };
}

