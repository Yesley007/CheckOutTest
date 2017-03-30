namespace Checkout.ApiServices.Drinks.RequestModels
{
    public class Search
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public string SortDirection { get; set; }
    }
}
