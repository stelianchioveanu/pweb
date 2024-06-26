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


import * as runtime from '../runtime';
import type {
  ProductAddDTO,
  ProductDTOPagedResponseRequestResponse,
  ProductDTORequestResponse,
  RequestResponse,
} from '../models';
import {
    ProductAddDTOFromJSON,
    ProductAddDTOToJSON,
    ProductDTOPagedResponseRequestResponseFromJSON,
    ProductDTOPagedResponseRequestResponseToJSON,
    ProductDTORequestResponseFromJSON,
    ProductDTORequestResponseToJSON,
    RequestResponseFromJSON,
    RequestResponseToJSON,
} from '../models';

export interface ApiProductAddProductPostRequest {
    productAddDTO?: ProductAddDTO;
}

export interface ApiProductDeleteProductIdDeleteRequest {
    id: string;
}

export interface ApiProductGetMyProductsGetRequest {
    search?: string;
    page?: number;
    pageSize?: number;
}

export interface ApiProductGetProductIdGetRequest {
    id: string;
}

export interface ApiProductGetProductsGetRequest {
    search?: string;
    page?: number;
    pageSize?: number;
}

/**
 * 
 */
export class ProductApi extends runtime.BaseAPI {

    /**
     */
    async apiProductAddProductPostRaw(requestParameters: ApiProductAddProductPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Product/AddProduct`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: ProductAddDTOToJSON(requestParameters.productAddDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProductAddProductPost(requestParameters: ApiProductAddProductPostRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiProductAddProductPostRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiProductDeleteProductIdDeleteRaw(requestParameters: ApiProductDeleteProductIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling apiProductDeleteProductIdDelete.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Product/DeleteProduct/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProductDeleteProductIdDelete(requestParameters: ApiProductDeleteProductIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiProductDeleteProductIdDeleteRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiProductGetMyProductsGetRaw(requestParameters: ApiProductGetMyProductsGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<ProductDTOPagedResponseRequestResponse>> {
        const queryParameters: any = {};

        if (requestParameters.search !== undefined) {
            queryParameters['Search'] = requestParameters.search;
        }

        if (requestParameters.page !== undefined) {
            queryParameters['Page'] = requestParameters.page;
        }

        if (requestParameters.pageSize !== undefined) {
            queryParameters['PageSize'] = requestParameters.pageSize;
        }

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Product/GetMyProducts`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProductDTOPagedResponseRequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProductGetMyProductsGet(requestParameters: ApiProductGetMyProductsGetRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<ProductDTOPagedResponseRequestResponse> {
        const response = await this.apiProductGetMyProductsGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiProductGetProductIdGetRaw(requestParameters: ApiProductGetProductIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<ProductDTORequestResponse>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling apiProductGetProductIdGet.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Product/GetProduct/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProductDTORequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProductGetProductIdGet(requestParameters: ApiProductGetProductIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<ProductDTORequestResponse> {
        const response = await this.apiProductGetProductIdGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiProductGetProductsGetRaw(requestParameters: ApiProductGetProductsGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<ProductDTOPagedResponseRequestResponse>> {
        const queryParameters: any = {};

        if (requestParameters.search !== undefined) {
            queryParameters['Search'] = requestParameters.search;
        }

        if (requestParameters.page !== undefined) {
            queryParameters['Page'] = requestParameters.page;
        }

        if (requestParameters.pageSize !== undefined) {
            queryParameters['PageSize'] = requestParameters.pageSize;
        }

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Product/GetProducts`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProductDTOPagedResponseRequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProductGetProductsGet(requestParameters: ApiProductGetProductsGetRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<ProductDTOPagedResponseRequestResponse> {
        const response = await this.apiProductGetProductsGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

}
