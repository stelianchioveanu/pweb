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
import type { ErrorMessage } from './ErrorMessage';
import {
    ErrorMessageFromJSON,
    ErrorMessageFromJSONTyped,
    ErrorMessageToJSON,
} from './ErrorMessage';
import type { ProductDTOPagedResponse } from './ProductDTOPagedResponse';
import {
    ProductDTOPagedResponseFromJSON,
    ProductDTOPagedResponseFromJSONTyped,
    ProductDTOPagedResponseToJSON,
} from './ProductDTOPagedResponse';

/**
 * 
 * @export
 * @interface ProductDTOPagedResponseRequestResponse
 */
export interface ProductDTOPagedResponseRequestResponse {
    /**
     * 
     * @type {ProductDTOPagedResponse}
     * @memberof ProductDTOPagedResponseRequestResponse
     */
    response?: ProductDTOPagedResponse;
    /**
     * 
     * @type {ErrorMessage}
     * @memberof ProductDTOPagedResponseRequestResponse
     */
    errorMessage?: ErrorMessage;
}

/**
 * Check if a given object implements the ProductDTOPagedResponseRequestResponse interface.
 */
export function instanceOfProductDTOPagedResponseRequestResponse(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function ProductDTOPagedResponseRequestResponseFromJSON(json: any): ProductDTOPagedResponseRequestResponse {
    return ProductDTOPagedResponseRequestResponseFromJSONTyped(json, false);
}

export function ProductDTOPagedResponseRequestResponseFromJSONTyped(json: any, ignoreDiscriminator: boolean): ProductDTOPagedResponseRequestResponse {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'response': !exists(json, 'response') ? undefined : ProductDTOPagedResponseFromJSON(json['response']),
        'errorMessage': !exists(json, 'errorMessage') ? undefined : ErrorMessageFromJSON(json['errorMessage']),
    };
}

export function ProductDTOPagedResponseRequestResponseToJSON(value?: ProductDTOPagedResponseRequestResponse | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'response': ProductDTOPagedResponseToJSON(value.response),
        'errorMessage': ErrorMessageToJSON(value.errorMessage),
    };
}
