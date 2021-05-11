using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.ServiceLocation;

namespace EPiServer.Reference.Commerce.Site.Helpers
{
    public static class CategoryHelper
    {
        private static readonly Injected<CategoryRepository> _categoryRepository;
        public static IList<Category> GetCategoriesByRootName(string rootName)
        {
            var root = _categoryRepository.Service.GetRoot();
            if (root != null)
            {
                return root.Categories.ToList();
            }

            return new List<Category>();
        }
        public static List<NodeContent> GetCategoriesFromContentArea(ContentArea contentArea, IContentLoader contentLoader)
        {
            var decendants = new List<NodeContent>();
            foreach (var contentAreaItem in contentArea.Items)
            {
                IContentData item;
                if (!contentLoader.TryGet(contentAreaItem.ContentLink, out item))
                {
                    continue;
                }
                var isNode = item as FashionNode;
                if (isNode != null) decendants.Add(isNode);
                var descendentReferences = contentLoader.GetDescendents(contentAreaItem.ContentLink);
                foreach (var reference in descendentReferences)
                {
                    var getItem = contentLoader.Get<IContent>(reference);
                    var fashionNode = getItem as FashionNode;
                    if (fashionNode != null) decendants.Add(fashionNode);
                }
            }
            return decendants;
        }
    }
}