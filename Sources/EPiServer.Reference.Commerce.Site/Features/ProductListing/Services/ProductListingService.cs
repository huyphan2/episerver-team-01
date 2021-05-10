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
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Blocks;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Pages;
using EPiServer.Reference.Commerce.Site.Helpers;
using EPiServer.Web.Routing;

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
        public ProductListViewModel GetListProduct(string brand, decimal priceFrom,decimal priceTo, string category, bool isSortDes,int pageNumber)
        {
            try
            {
                var result = new ProductListViewModel();
                result.TotalProducts = MatchFilter(brand, priceFrom,priceTo, category, isSortDes).GetContentResult().TotalMatching;
                result.PageSize = PageSize;
                var products = MatchFilter(brand, priceFrom, priceTo, category, isSortDes).Skip(PageSize*(pageNumber-1)).Take(PageSize).GetContentResult();
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
                Price = currentBlock.PriceFilter.ToList(),
                Brands = GetBrands(currentBlock.BrandCollection),
                Categories = GetCategories(currentBlock.CategoryCollection)
            };
            return model;
        }

        public List<string> GetBrands(ContentArea brandArea)
        {
            var decendants = new List<FashionProduct>();
            foreach (var contentAreaItem in brandArea.Items)
            {
                IContentData item;
                if (!_contentLoader.TryGet(contentAreaItem.ContentLink, out item))
                {
                    continue;
                }
                var fashionNodeContent = item as FashionNode;
                var catalogNodeContent = item as CatalogContent;
                if (fashionNodeContent != null ) PageHelper.GetDescendantsOfBrand<FashionProduct>(fashionNodeContent.ContentLink,decendants);
                else if(catalogNodeContent != null) PageHelper.GetDescendantsOfBrand<FashionProduct>(catalogNodeContent.ContentLink, decendants);
            }

            var result = decendants.Select(x => x.Brand).Distinct().ToList();
            return result;
        }

        public List<string> GetCategories(ContentArea categoryArea)
        {
            var result = new List<string>();
            foreach (var contentAreaItem in categoryArea.Items)
            {
                IContentData item;
                if (!_contentLoader.TryGet(contentAreaItem.ContentLink, out item))
                {
                    continue;
                }
                var nodeContent = item as FashionNode;
                if (nodeContent == null) continue;
                result.Add(_contentLoader.Get<FashionNode>(nodeContent.ContentLink).DisplayName);
                result.AddRange(_contentLoader.GetChildren<FashionNode>(nodeContent.ContentLink).Select(x => x.DisplayName));
            }
            return result.Distinct().ToList();
        }
        public List<string> ProductCategories(IEnumerable<ContentReference> categories)
        {
            if (categories == null) return null;
            return categories.Select(contentReference => _contentLoader.Get<FashionNode>(contentReference).DisplayName)
                .Distinct().ToList();
        }
        public ITypeSearch<FashionProduct> MatchFilter(string brand, decimal priceFrom, decimal priceTo, string category, bool isSortDes)
        {
            var search = _episerverFindService.EpiClient().Search<FashionProduct>();
            var requiredFilter = new FilterBuilder<FashionProduct>(search.Client);

            if (!string.IsNullOrEmpty(brand)) requiredFilter = requiredFilter.And(x => x.Brand.Match(brand));
            if(priceTo!=0) requiredFilter = requiredFilter.And(x => x.Price.InRange(priceFrom, priceTo));
            if (!string.IsNullOrEmpty(category))
            {
                requiredFilter = requiredFilter.And(x => x.ListCategories.Match(category));
            }

            search = isSortDes ? search.OrderByDescending(x => x.DisplayName) : search.OrderBy(x => x.DisplayName);
            return search.Filter(requiredFilter);
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
                if (!word.Equals(wholeWordWildCards))
                {
                    search = search.WildcardSearch(word, x => x.DisplayName, 700)
                        .WildcardSearch(word, x => x.Brand, 600)
                        .WildcardSearch(word, x => x.ListCategories.FirstOrDefault(), 500);
                }
            }

            var result = new List<string>();
            foreach (var fashionProduct in search.GetContentResult())
            {
                result.Add(fashionProduct.DisplayName);
            }

            return result;
        }
    }
}