using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Commerce;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Blocks;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Models;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Pages;
using EPiServer.Reference.Commerce.Site.Helpers;
using EPiServer.Web.Routing;
using FashionNode = EPiServer.Reference.Commerce.Site.Features.Product.Models.FashionNode;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    [ServiceConfiguration(ServiceType = typeof(IProductListingService))]
    public class ProductListingService : IProductListingService
    {
        private readonly IPageRouteHelper pageRouteHelper;
        private readonly IEpiserverFindService _episerverFindService;
        private readonly IProductService _productService;
        private readonly IContentLoader _contentLoader;
        private const int PageSize = 9;
        public ProductListingService(IEpiserverFindService episerverFindService, IContentLoader contentLoader, IProductService productService, IPageRouteHelper pageRouteHelper)
        {
            _episerverFindService = episerverFindService;
            _contentLoader = contentLoader;
            _productService = productService;
            this.pageRouteHelper = pageRouteHelper;
        }
        public ProductListViewModel GetListProduct(string brand, decimal priceFrom, decimal priceTo, string category, bool isSortDes, int pageNumber)
        {
            try
            {
                var result = new ProductListViewModel();
                //var uiCurrentCulture = EPiServer.Globalization.GlobalizationSettings.UICultureLanguageCode;
                var uiCurrentCulture = Thread.CurrentThread.CurrentUICulture.Name;
                result.TotalProducts = MatchFilter(brand, priceFrom, priceTo, category, isSortDes, uiCurrentCulture).GetContentResult().TotalMatching;
                result.PageSize = PageSize;
                var products = MatchFilter(brand, priceFrom, priceTo, category, isSortDes, uiCurrentCulture).Skip(PageSize * (pageNumber - 1)).Take(PageSize).GetContentResult();
                foreach (var item in products)
                {
                    var product = _productService.GetProductTileViewModel(item);
                    if (product != null) result.Products.Add(product);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public FilterParams GetFilterParams(ProductListBlock currentBlock)
        {
            var model = new FilterParams()
            {
                Price = currentBlock.PriceFilter?.ToList(),
                Brands = GetBrands(currentBlock.BrandCollection),
                Categories = GetCategories(currentBlock.CategoryCollection)
            };
            return model;
        }

        public List<string> GetBrands(ContentArea contentArea)
        {
            var products = PageHelper.GetProductsFromContentArea(contentArea, _contentLoader);
            var result = products.Select(x => x.Brand).Distinct().OrderBy(x => x).ToList();
            return result;
        }

        public List<string> GetCategories(ContentArea categoryArea)
        {
            var result = new List<string>();
            var decendants = CategoryHelper.GetCategoriesFromContentArea(categoryArea, _contentLoader);
            return decendants.Select(x => x.DisplayName).Distinct().OrderBy(x => x).ToList();
        }
        public List<string> ProductCategories(IEnumerable<ContentReference> categories)
        {
            if (categories == null) return null;
            return categories.Select(contentReference => _contentLoader.Get<FashionNode>(contentReference).DisplayName)
                .Distinct().ToList();
        }
        public ITypeSearch<FashionProduct> MatchFilter(string brand, decimal priceFrom, decimal priceTo, string category, bool isSortDes, string language)
        {
            var search = _episerverFindService.EpiClient().Search<FashionProduct>();
            var requiredFilter = new FilterBuilder<FashionProduct>(search.Client);
            requiredFilter = requiredFilter.FilterOnCurrentMarket().And(x => x.Language.Name.MatchCaseInsensitive(language));
            if (!string.IsNullOrEmpty(brand)) requiredFilter = requiredFilter.And(x => x.Brand.MatchCaseInsensitive(brand));
            if (priceTo != 0) requiredFilter = requiredFilter.And(x => x.Price.InRange(priceFrom, priceTo));
            if (!string.IsNullOrEmpty(category))
            {
                requiredFilter = requiredFilter.And(x => x.ListCategories.MatchCaseInsensitive(category));
            }

            search = isSortDes ? search.OrderByDescending(x => x.DisplayName) : search.OrderBy(x => x.DisplayName);
            return search.Filter(requiredFilter);
        }

        public List<ProductTileViewModel> SearchWildcardProduct(string query, string language)
        {
            string wholeWordWildCards = WildCardExtensions.WrapInAsterisks(query);

            var words = query.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(WildCardExtensions.WrapInAsterisks)
                .ToList();
            var search = _episerverFindService.EpiClient().Search<FashionProduct>();
            search = search.Filter(x => x.Language.Name.Match(language));
            search = search.WildcardSearch<FashionProduct>(wholeWordWildCards, x => x.DisplayName, 1000)
                .WildcardSearch<FashionProduct>(wholeWordWildCards, x => x.Brand, 900)
                .WildcardSearch<FashionProduct>(wholeWordWildCards, x => x.ListCategories.FirstOrDefault(), 800);
            int priorityBoost = 700;
            foreach (var word in words)
            {
                if (!word.Equals(wholeWordWildCards))
                {
                    if (priorityBoost == 0) break;
                    search = search.WildcardSearch(word, x => x.DisplayName, --priorityBoost);
                }
            }

            var result = new List<ProductTileViewModel>();
            foreach (var fashionProduct in search.GetContentResult())
            {
                var productViewModel = _productService.GetProductTileViewModel(fashionProduct);
                if (productViewModel != null) result.Add(productViewModel);
            }

            return result;
        }

    }
}