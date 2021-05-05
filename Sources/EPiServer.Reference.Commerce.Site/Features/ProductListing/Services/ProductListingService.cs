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

        public ProductListingService(IEpiserverFindService episerverFindService)
        {
            _episerverFindService = episerverFindService;
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

        public FilterParams GetFilterParams()
        {
            return null;
        }
    }
}