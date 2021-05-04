using System.Collections.Generic;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    public interface IProductListingService
    {
        IEnumerable<ProductTileViewModel> GetListProduct();
        FilterParams GetFilterParams();

    }
}