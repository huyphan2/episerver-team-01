using System.Web;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    public static class StringConstants
    {
        public static string Https = "https://";
        public static string ProductListingApiUrl = "/api/productlist/GetProductList";
        public static string CurrentHostName = HttpContext.Current.Request.Url.Host;

    }
}