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
 * @interface AddressDTO
 */
export interface AddressDTO {
    /**
     * 
     * @type {string}
     * @memberof AddressDTO
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof AddressDTO
     */
    streetName?: string | null;
    /**
     * 
     * @type {string}
     * @memberof AddressDTO
     */
    city?: string | null;
    /**
     * 
     * @type {number}
     * @memberof AddressDTO
     */
    number?: number;
    /**
     * 
     * @type {string}
     * @memberof AddressDTO
     */
    phoneNumber?: string | null;
}

/**
 * Check if a given object implements the AddressDTO interface.
 */
export function instanceOfAddressDTO(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function AddressDTOFromJSON(json: any): AddressDTO {
    return AddressDTOFromJSONTyped(json, false);
}

export function AddressDTOFromJSONTyped(json: any, ignoreDiscriminator: boolean): AddressDTO {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'streetName': !exists(json, 'streetName') ? undefined : json['streetName'],
        'city': !exists(json, 'city') ? undefined : json['city'],
        'number': !exists(json, 'number') ? undefined : json['number'],
        'phoneNumber': !exists(json, 'phoneNumber') ? undefined : json['phoneNumber'],
    };
}

export function AddressDTOToJSON(value?: AddressDTO | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'streetName': value.streetName,
        'city': value.city,
        'number': value.number,
        'phoneNumber': value.phoneNumber,
    };
}
