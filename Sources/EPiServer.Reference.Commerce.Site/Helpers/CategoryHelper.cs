using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.DataAbstraction;
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
    }
}