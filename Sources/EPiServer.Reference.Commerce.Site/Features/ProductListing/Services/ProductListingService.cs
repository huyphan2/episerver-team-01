using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    public class ProductListingService : IProductListingService
    {
        public IEnumerable<ProductTileViewModel> GetListProduct()
        {
            throw new NotImplementedException();
        }

        public FilterParams GetFilterParams()
        {
            throw new NotImplementedException();
        }
    }
}