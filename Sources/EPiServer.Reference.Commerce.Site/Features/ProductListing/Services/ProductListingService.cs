using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Commerce;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
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
        private readonly IContentLoader _contentLoader;


        public ProductListingService(IEpiserverFindService episerverFindService, IContentLoader contentLoader)
        {
            _episerverFindService = episerverFindService;
            _contentLoader = contentLoader;
        }


        public IEnumerable<ProductTileViewModel> GetListProduct(string brand,string category, decimal price)
        {
            var filter = EpiserverFind.Instance.Create().BuildFilter<FashionProduct>();

            var client = _episerverFindService.GetFashionCurrentMarket();
            if(brand!="") filter = filter.And(x => x.Brand.Match(brand));
           // if (price!=0) filter = filter.And(x => x.Brand.Match(brand));
            var check = _episerverFindService.EpiClient().Search<FashionProduct>().Filter(x => filter)
                .GetContentResult();
            
            return new List<ProductTileViewModel>();
        }

        public FilterParams GetFilterParams(Pages.ProductListing currentProductListing)
        {
            var model = new FilterParams();
            var prices = _contentLoader.Get<Pages.ProductListing>(currentProductListing.ContentLink).PriceFilter;
            var brand = _episerverFindService.EpiClient().Search<FashionProduct>().OrderBy(x => x.Brand)
                .Select(x => x.Brand).GetResult();
            int totalBrand = brand.TotalMatching;
            var categories = _episerverFindService.EpiClient().Search<FashionNode>().OrderBy(x=>x.Name).Select(x=>x.Name).GetResult();
            int totalCategory = categories.TotalMatching;
            model.Price = prices.ToList();
            model.Brands = _episerverFindService.EpiClient().Search<FashionProduct>().OrderBy(x => x.Brand)
                .Select(x => x.Brand).Take(totalBrand).GetResult().Distinct().ToList();
            model.Categories = _episerverFindService.EpiClient().Search<FashionNode>().OrderBy(x => x.Name).Select(x => x.Name).Take(totalCategory).GetResult().Distinct().ToList();
            return model;
        }
    }
}