import { useAppSelector } from "@application/store";
import { ApiProductGetMyProductsGetRequest, ProductAddDTO, ProductApi } from "../client";
import { getAuthenticationConfiguration } from "@infrastructure/utils/userUtils";

/**
 * Use constants to identify mutations and queries.
 */
const getProductsQueryKey = "getProductsQuery";
const getProductQueryKey = "getProductQuery";
const addProductMutationKey = "addProductMutation";
const deleteProductMutationKey = "deleteProductMutation";

/**
 * Returns the an object with the callbacks that can be used for the React Query API, in this case to manage the user API.
 */
export const useProductApi = () => {
    const { token } = useAppSelector(x => x.profileReducer); // You can use the data form the Redux storage. 
    const config = getAuthenticationConfiguration(token); // Use the token to configure the authentication header.

    const getProducts = (page: ApiProductGetMyProductsGetRequest) => new ProductApi(config).apiProductGetMyProductsGet(page); // Use the generated client code and adapt it.
    const getProduct = (id: string) => new ProductApi(config).apiProductGetProductIdGet({ id });
    const addProduct = (product: ProductAddDTO) => new ProductApi(config).apiProductAddProductPost({ productAddDTO: product });
    const deleteProduct = (id: string) => new ProductApi(config).apiProductDeleteProductIdDelete({ id });

    return {
        getProducts: { // Return the query object.
            key: getProductsQueryKey, // Add the key to identify the query.
            query: getProducts // Add the query callback.
        },
        getProduct: {
            key: getProductQueryKey,
            query: getProduct
        },
        addProduct: { // Return the mutation object.
            key: addProductMutationKey, // Add the key to identify the mutation.
            mutation: addProduct // Add the mutation callback.
        },
        deleteProduct: {
            key: deleteProductMutationKey,
            mutation: deleteProduct
        }
    }
}