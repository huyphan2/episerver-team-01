using EPiServer.Reference.Commerce.Site.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Helpers
{
    public class ApiHelper
    {
        public static string Call(string url) => $"{StringConstants.Https}{StringConstants.CurrentHostName}/api/{url}";
    }

    public static class ApiUrl
    {
        public const string OrderGet = "order/get";
        public const string OrderSearch = "order/search";
    }

}