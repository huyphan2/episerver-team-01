using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    public interface IProductListingService
    {
        IEnumerable<ProductTileViewModel> GetListProduct(string brand, decimal price, string category);
        FilterParams GetFilterParams(Pages.ProductListing currentProductListing);
        List<string> GetCategories();
        List<string> GetBrands();
        List<string> ProductCategories(IEnumerable<ContentReference> categories);

    }
}