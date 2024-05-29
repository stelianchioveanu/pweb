import { useAppSelector } from "@application/store";
import { ApiProductTagGetTagsGetRequest, ProductTagDTO, ProductTagApi, ProductTagAddDTO } from "../client";
import { getAuthenticationConfiguration } from "@infrastructure/utils/userUtils";

/**
 * Use constants to identify mutations and queries.
 */
const getTagsQueryKey = "getTagsQuery";
const getTagQueryKey = "getTagQuery";
const addTagMutationKey = "addTagMutation";
const deleteTagMutationKey = "deleteTagMutation";

/**
 * Returns the an object with the callbacks that can be used for the React Query API, in this case to manage the user API.
 */
export const useProductTagsApi = () => {
    const { token } = useAppSelector(x => x.profileReducer); // You can use the data form the Redux storage. 
    const config = getAuthenticationConfiguration(token); // Use the token to configure the authentication header.

    const getTags = (page: ApiProductTagGetTagsGetRequest) => new ProductTagApi(config).apiProductTagGetTagsGet(page); // Use the generated client code and adapt it.
    //const getTag = (id: string) => new ProductTagApi(config).api({ id });
    const addTag = (tag: ProductTagAddDTO) => new ProductTagApi(config).apiProductTagAddProductTagPost({ productTagAddDTO: tag });
    const deleteTag = (id: string) => new ProductTagApi(config).apiProductTagDeleteProductTagIdDelete({ id });

    return {
        getTags: { // Return the query object.
            key: getTagsQueryKey, // Add the key to identify the query.
            query: getTags // Add the query callback.
        },
        // getProduct: {
        //     key: getProductQueryKey,
        //     query: getProduct
        // },
        addTag: { // Return the mutation object.
            key: addTagMutationKey, // Add the key to identify the mutation.
            mutation: addTag // Add the mutation callback.
        },
        deleteTag: {
            key: deleteTagMutationKey,
            mutation: deleteTag
        }
    }
}