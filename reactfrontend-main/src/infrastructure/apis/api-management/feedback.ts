import { useAppSelector } from "@application/store";
import { ApiFeedbackGetFeedbacksGetRequest, FeedbackAddDTO, FeedbackApi } from "../client";
import { getAuthenticationConfiguration } from "@infrastructure/utils/userUtils";

/**
 * Use constants to identify mutations and queries.
 */
const getFeedbacksQueryKey = "getFeedbacksQuery";
const getFeedbackQueryKey = "getFeedbackQuery";
const addFeedbackMutationKey = "addFeedbackMutation";
//const deleteProductMutationKey = "deleteProductMutation";

/**
 * Returns the an object with the callbacks that can be used for the React Query API, in this case to manage the user API.
 */
export const useFeedbackApi = () => {
    const { token } = useAppSelector(x => x.profileReducer); // You can use the data form the Redux storage. 
    const config = getAuthenticationConfiguration(token); // Use the token to configure the authentication header.

    const getFeedbacks = (page: ApiFeedbackGetFeedbacksGetRequest) => new FeedbackApi(config).apiFeedbackGetFeedbacksGet(page); // Use the generated client code and adapt it.
    // const getProduct = (id: string) => new FeedbackApi(config).({ id });
    const addFeedback = (feedback: FeedbackAddDTO) => new FeedbackApi(config).apiFeedbackAddFeedbackPost({ feedbackAddDTO: feedback });
    //const deleteProduct = (id: string) => new ProductApi(config).apiProductDeleteProductIdDelete({ id });

    return {
        getFeedbacks: { // Return the query object.
            key: getFeedbacksQueryKey, // Add the key to identify the query.
            query: getFeedbacks // Add the query callback.
        },
        // getProduct: {
        //     key: getProductQueryKey,
        //     query: getProduct
        // },
        addFeedback: { // Return the mutation object.
            key: addFeedbackMutationKey, // Add the key to identify the mutation.
            mutation: addFeedback // Add the mutation callback.
        },
        // deleteProduct: {
        //     key: deleteProductMutationKey,
        //     mutation: deleteProduct
        // }
    }
}