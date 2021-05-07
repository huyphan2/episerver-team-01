using EPiServer.Reference.Commerce.Site.Infrastructure;
using EPiServer.Web.Routing;

namespace EPiServer.Reference.Commerce.Site.Helpers
{
    public static class PageHelper
    {
        public static bool CheckIsCurrentPage(string page)
        {
            if (GetCurrentPageType().Equals(page)) return true;
            return false;
        }
        public static string GetCurrentPageType()
        {
            var pageRouteHelper = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IPageRouteHelper>();
            var currentPageType = pageRouteHelper.Page.PageTypeName;
            return currentPageType;
        }
    }
}