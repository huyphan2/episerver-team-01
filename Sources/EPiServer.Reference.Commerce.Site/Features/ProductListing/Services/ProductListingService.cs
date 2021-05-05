using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Commerce;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.ServiceLocation;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    [ServiceConfiguration(ServiceType = typeof(IProductListingService))]
    public class ProductListingService : IProductListingService
    {
        private readonly IEpiserverFindService _episerverFindService;
        private readonly IProductService _productService;
        private readonly IContentLoader _contentLoader;


        public ProductListingService(IEpiserverFindService episerverFindService, IContentLoader contentLoader, IProductService productService)
        {
            _episerverFindService = episerverFindService;
            _contentLoader = contentLoader;
            _productService = productService;
        }


        public IEnumerable<ProductTileViewModel> GetListProduct(string brand,string category, decimal price)
        {
            try
            {
                var result = new List<ProductTileViewModel>();
                var filter = EpiserverFind.Instance.Create().BuildFilter<FashionProduct>();
                //var client = _episerverFindService.GetFashionCurrentMarket();
                if (!string.IsNullOrEmpty(brand)) filter = filter.And(x => x.Brand.Match(brand));
                decimal low = price < 500 ? 0 : price-500;
                decimal hight = price + 500;
                if (price != 0) filter = filter.And(x => x.Price.InRange(low, hight));
                //if (!string.IsNullOrEmpty(category)) filter = filter.And(x => x.Categorie.(brand));                
                var check = _episerverFindService.EpiClient().Search<FashionProduct>().Filter(x => filter)
                    .GetContentResult();
                foreach (var item in check)
                {
                    var product = _productService.GetProductTileViewModel(item);
                    if (product != null) result.Add(product);
                }
                return result;
            }
            catch(Exception e)
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
            return _episerverFindService.EpiClient().Search<FashionProduct>().OrderBy(x => x.Brand)
                .Select(x => x.Brand).Take(totalBrand).GetResult().Distinct().ToList();
        }

        public List<string> GetCategories()
        {
            var totalCategory = _episerverFindService.EpiClient().Search<FashionNode>().Select(x => x.Name).GetResult().TotalMatching;
            return _episerverFindService.EpiClient().Search<FashionNode>().OrderBy(x => x.Name)
                .Select(x => x.Name).Take(totalCategory).GetResult().Distinct().ToList();
        }
    }
}