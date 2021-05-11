using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Blocks;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Services
{
    public interface IProductListingService
    {
        ProductListViewModel GetListProduct(string brand, decimal priceFrom, decimal priceTo, string category, bool isSortDes, int pageNumber);
        FilterParams GetFilterParams(ProductListBlock currentBlock);
        List<string> GetCategories(ContentArea categoryArea);
        List<string> GetBrands(ContentArea brandArea);
        List<string> ProductCategories(IEnumerable<ContentReference> categories);
        ITypeSearch<FashionProduct> MatchFilter(string brand, decimal priceFrom, decimal priceTo, string category, bool isSortDes, string language);
        List<ProductTileViewModel> SearchWildcardProduct(string query, string language);
    }
}