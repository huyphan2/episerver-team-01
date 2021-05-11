using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;

namespace EPiServer.Reference.Commerce.Site.Features.Product.Services
{
    public interface IProductService: IEpiserverFindService
    {
        ProductTileViewModel GetProductTileViewModel(EntryContentBase entry);
        ProductTileViewModel GetProductTileViewModel(ContentReference contentLink);
        string GetSiblingVariantCodeBySize(string siblingCode, string size);
        List<ProductTileViewModel> GetBestSellerFasionProduct();
        List<ProductTileViewModel> GetNewestFasionProduct();
        List<ProductTileViewModel> GetFasionProductByCategoryAndSorting(string category, string orderField, int numberOfItem);
    }
}