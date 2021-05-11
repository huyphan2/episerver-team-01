using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.Commerce.Order;

namespace EPiServer.Reference.Commerce.Site.Features.Product.Services
{
    public interface IProductService : IEpiserverFindService
    {
        ProductTileViewModel GetProductTileViewModel(EntryContentBase entry);
        ProductTileViewModel GetProductTileViewModel(ContentReference contentLink);
        string GetSiblingVariantCodeBySize(string siblingCode, string size);
        List<ProductTileViewModel> GetBestSellerFasionProduct();
        List<ProductTileViewModel> GetNewestFasionProduct();       
  		IEnumerable<ProductTileViewModel> GetRelatedProducts(FashionProduct product, int size = 12);
        IEnumerable<ProductTileViewModel> GetMayLikeProducts(FashionProduct product, IEnumerable<ILineItem> lineItems, int size = 12);
		List<ProductTileViewModel> GetFasionProductByCategoryAndSorting(string language, string category, string orderField, int numberOfItem);
    }
}