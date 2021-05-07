using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    public interface IProductListingService
    {
        ProductListViewModel GetListProduct(string brand, decimal price, string category, bool isSortDes,int pageNumber);
        FilterParams GetFilterParams(Pages.ProductListing currentProductListing);
        List<string> GetCategories();
        List<string> GetBrands();
        List<string> ProductCategories(IEnumerable<ContentReference> categories);
        ITypeSearch<FashionProduct> MatchFilter(string brand, decimal price, string category, bool isSortDes);
        List<string> GetProductNameByText(string text);
    }
}