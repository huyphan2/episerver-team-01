using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
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
        public static List<string> GetCategoriesFromItemCollection(this LinkItemCollection linkItemCollection)
        {

            var pages = new List<string>();

            if (linkItemCollection != null)
            {
                var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
                foreach (var linkItem in linkItemCollection)
                {
                    if (String.IsNullOrEmpty(linkItem.Href))
                        continue;

                    string linkUrl;
                    if (Web.Routing.UrlResolver.Current.TryToPermanent(linkItem.Href, out linkUrl))
                    {
                        var urlBuilder = new UrlBuilder(linkUrl);
                        var page = urlResolver.Route(urlBuilder) as FashionNode;
                        if (page != null)
                        {
                            var categories=CategoryHelper.GetCategoriesByRootName(page.Name);
                            foreach (var category in categories)
                            {
                                pages.Add(category.Name);
                            }
                        }
                    }
                }
            }
            return pages.Distinct().ToList();
        }
        public static void GetDescendantsOfType<T>(ContentReference contentLink, ICollection<T> descendants,CultureInfo language, IContentLoader contentLoader) where T : class
        {
            var children = contentLoader.GetChildren<NodeContent>(contentLink,language);
            if (children.Any())
            {
                foreach (var child in children)
                {
                    if (child is T)
                    {
                        descendants.Add(child as T);
                    }
                    GetDescendantsOfType(child.ContentLink, descendants, language, contentLoader);
                }
            }
        }
        public static List<FashionProduct> GetProductsFromContentArea(ContentArea contentArea, IContentLoader contentLoader)
        {
            var products = new List<FashionProduct>();
            foreach (var contentAreaItem in contentArea.Items)
            {
                IContentData item;
                if (!contentLoader.TryGet(contentAreaItem.ContentLink, out item))
                {
                    continue;
                }
                var descendentReferences = contentLoader.GetDescendents(contentAreaItem.ContentLink);
                foreach (var reference in descendentReferences)
                {
                    var getItem = contentLoader.Get<IContent>(reference);
                    var fashionProduct = getItem as FashionProduct;
                    if (fashionProduct != null) products.Add(fashionProduct);
                }
            }
            return products;
        }
    }
}