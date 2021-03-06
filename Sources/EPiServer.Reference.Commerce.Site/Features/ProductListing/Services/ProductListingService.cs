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
using System.Linq;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    [ServiceConfiguration(ServiceType = typeof(IProductListingService))]
    public class ProductListingService : IProductListingService
    {
        private readonly IEpiserverFindService _episerverFindService;
        private readonly IProductService _productService;
        private readonly IContentLoader _contentLoader;
        private const int PageSize = 9;
        public ProductListingService(IEpiserverFindService episerverFindService, IContentLoader contentLoader, IProductService productService)
        {
            _episerverFindService = episerverFindService;
            _contentLoader = contentLoader;
            _productService = productService;
        }
        public ProductListViewModel  GetListProduct(string brand, decimal price, string category, bool isSortDes,int pageNumber)
        {
            try
            {
                var result = new ProductListViewModel();
                result.TotalProducts = MatchFilter(brand, price, category, isSortDes).GetContentResult().TotalMatching;
                result.PageSize = PageSize;
                var products = MatchFilter(brand, price, category, isSortDes).Skip(PageSize*(pageNumber-1)).Take(PageSize).GetContentResult();
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

        public FilterParams GetFilterParams(Pages.ProductListing currentProductListing)
        {
            var prices = _contentLoader.Get<Pages.ProductListing>(currentProductListing.ContentLink).PriceFilter;
            var model = new FilterParams()
            {
                Price = prices.ToList(),
                Brands = GetBrands(),
                Categories = GetCategories()
            };
            return model;
        }

        public List<string> GetBrands()
        {
            var totalBrand = _episerverFindService.EpiClient().Search<FashionProduct>().Select(x => x.Brand).GetResult().TotalMatching;
            var brands = _episerverFindService.EpiClient().Search<FashionProduct>().OrderBy(x => x.Brand)
                .Select(x => x.Brand).Take(totalBrand).GetResult().Distinct().ToList();
            return brands ?? null;
        }


        public List<string> GetCategories()
        {
            var totalCategory = _episerverFindService.EpiClient().Search<FashionNode>().Select(x => x.DisplayName).GetResult().TotalMatching;
            var categories = _episerverFindService.EpiClient().Search<FashionNode>().OrderBy(x => x.DisplayName)
                .Select(x => x.Name).Take(totalCategory).GetResult().Distinct().ToList();
            return categories ?? null;
        }
        public List<string> ProductCategories(IEnumerable<ContentReference> categories)
        {
            if (categories == null) return null;
            return categories.Select(contentReference => _contentLoader.Get<FashionNode>(contentReference).DisplayName)
                .Distinct().ToList();
        }
        public ITypeSearch<FashionProduct> MatchFilter(string brand, decimal price, string category, bool isSortDes)
        {
            var search = _episerverFindService.EpiClient().Search<FashionProduct>();
            var requiredFilter = new FilterBuilder<FashionProduct>(search.Client);

            if (!string.IsNullOrEmpty(brand)) requiredFilter = requiredFilter.And(x => x.Brand.Match(brand));
            decimal low = price < 10 ? 0 : price - 10;
            decimal hight = price + 10;
            if (price != 0) requiredFilter = requiredFilter.And(x => x.Price.InRange(low, hight));
            if (!string.IsNullOrEmpty(category))
            {
                requiredFilter = requiredFilter.And(x => x.ListCategories.Match(category));
            }

            search = isSortDes ? search.OrderByDescending(x => x.DisplayName) : search.OrderBy(x => x.DisplayName);
            return search.Filter(requiredFilter);
        }

        public List<string> GetProductNameByText(string text)
        {
            throw new NotImplementedException();
        }

        public List<string> SearchWildcardProduct(string query)
        {
            string wholeWordWildCards = WildCardExtensions.WrapInAsterisks(query);

            var words = query.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(WildCardExtensions.WrapInAsterisks)
                .ToList();
            var search = _episerverFindService.EpiClient().Search<FashionProduct>();

            search=search.WildcardSearch<FashionProduct>(wholeWordWildCards, x => x.DisplayName, 1000)
                .WildcardSearch<FashionProduct>(wholeWordWildCards, x => x.Brand, 900)
                .WildcardSearch<FashionProduct>(wholeWordWildCards, x => x.ListCategories.FirstOrDefault(), 800);
            foreach (var word in words)
            {
                search = search.WildcardSearch(word, x => x.DisplayName, 700)
                    .WildcardSearch(word, x => x.Brand, 600)
                    .WildcardSearch(word, x => x.ListCategories.FirstOrDefault(), 500);
            }
            //search.GetContentResult()
            return new List<string>();
        }
    }
}