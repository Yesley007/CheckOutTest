using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.Drinks.ResponseModels;
using Checkout.ApiServices.Drinks.RequestModels;
using Checkout.Utilities;

namespace Checkout.ApiServices.Drinks
{
    public class DrinkService
    {
        public HttpResponse<DrinkResponse> AddDrink(Drink drink)
        {
            return new ApiHttpClient().PostRequest<DrinkResponse>(ApiUrls.DrinkAdd, AppSettings.SecretKey, drink);
        }

        public HttpResponse<DrinkResponse> UpdateDrinkStock(Drink drink)
        {
            return new ApiHttpClient().PutRequest<DrinkResponse>(ApiUrls.DrinkUpdate, AppSettings.SecretKey, drink);
        }

        public HttpResponse<DrinkResponse> DeleteDrink(string drinkName)
        {
            return new ApiHttpClient().DeleteRequest<DrinkResponse>(string.Format(ApiUrls.DrinkDelete, drinkName), AppSettings.SecretKey);

        }

        public HttpResponse<DrinkResponse> GetAllDrink()
        {
            return new ApiHttpClient().GetRequest<DrinkResponse>(string.Format(ApiUrls.DrinkGetAll), AppSettings.SecretKey);
        }
        public HttpResponse<DrinkResponse> GetDrink(string drinkName)
        {
            return new ApiHttpClient().GetRequest<DrinkResponse>(string.Format(ApiUrls.DrinkGetDrink,drinkName), AppSettings.SecretKey);
        }

        public HttpResponse<DrinkResponse> GetCustomDrink(Search searchCriteria)
        {
            var customSearchUri = string.Format(ApiUrls.DrinkGetCustom, searchCriteria.PageSize, searchCriteria.PageNumber);
            if (!string.IsNullOrEmpty(searchCriteria.OrderBy) && !string.IsNullOrWhiteSpace(searchCriteria.OrderBy))
            {
                customSearchUri = UrlHelper.AddParameterToUrl(customSearchUri, "OrderBy", searchCriteria.OrderBy);
            }
            if (!string.IsNullOrEmpty(searchCriteria.SortDirection) && !string.IsNullOrWhiteSpace(searchCriteria.SortDirection))
            {
                customSearchUri = UrlHelper.AddParameterToUrl(customSearchUri, "sortDirection", searchCriteria.SortDirection);
            }

            return new ApiHttpClient().GetRequest<DrinkResponse>(customSearchUri, AppSettings.SecretKey);
        }
    }
}
