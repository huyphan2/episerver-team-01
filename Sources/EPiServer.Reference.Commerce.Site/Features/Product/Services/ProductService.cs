using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework;
using EPiServer.Find.Helpers;
using EPiServer.Reference.Commerce.Site.Features.Cart.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModelFactories;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Shared.Extensions;
using EPiServer.Reference.Commerce.Site.Features.Shared.Services;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPiServer.Reference.Commerce.Site.Features.Product.Services
{
    [ServiceConfiguration(typeof(IProductService), Lifecycle = ServiceInstanceScope.Singleton)]
    public class ProductService : EpiserverFindService, IProductService
    {
        private readonly IContentLoader _contentLoader;
        private readonly IPricingService _pricingService;
        private readonly UrlResolver _urlResolver;
        private readonly CatalogContentService _catalogContentService;
        private readonly CatalogEntryViewModelFactory _catalogEntryViewModelFactory;
        private readonly IRelationRepository _relationRepository;
        private readonly ReferenceConverter _referenceConverter;
        public ProductService(IContentLoader contentLoader,
            IPricingService pricingService,
            UrlResolver urlResolver,
            CatalogEntryViewModelFactory catalogEntryViewModelFactory,
            CatalogContentService catalogContentService,
            IRelationRepository relationRepository,
            ReferenceConverter referenceConverter)
        {
            _contentLoader = contentLoader;
            _pricingService = pricingService;
            _urlResolver = urlResolver;
            _catalogContentService = catalogContentService;
            _catalogEntryViewModelFactory = catalogEntryViewModelFactory;
            _relationRepository = relationRepository;
            _referenceConverter = referenceConverter;
        }

        public string GetSiblingVariantCodeBySize(string siblingCode, string size)
        {
            var siblingVariants = _catalogContentService.GetSiblingVariants<FashionVariant>(siblingCode).ToList();
            var siblingVariant = siblingVariants.First(x => x.Code == siblingCode);

            foreach (var variant in siblingVariants)
            {
                if (variant.Size.Equals(size, StringComparison.OrdinalIgnoreCase) && variant.Code != siblingCode
                    && variant.Color.Equals(siblingVariant.Color, StringComparison.OrdinalIgnoreCase))
                {
                    return variant.Code;
                }
            }

            return null;
        }

        public virtual ProductTileViewModel GetProductTileViewModel(ContentReference contentLink)
        {
            return GetProductTileViewModel(_catalogContentService.Get<EntryContentBase>(contentLink));
        }

        public virtual ProductTileViewModel GetProductTileViewModel(EntryContentBase entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if (entry is PackageContent)
            {
                return CreateProductViewModelForEntry((PackageContent)entry);
            }

            if (entry is ProductContent)
            {
                var product = (ProductContent)entry;
                var variant = _catalogContentService.GetFirstVariant<FashionVariant>(product);

                return CreateProductViewModelForVariant(product, variant);
            }

            if (entry is VariationContent)
            {
                var product = _catalogContentService.GetParentProduct<ProductContent>(entry);
                return CreateProductViewModelForVariant(product, (VariationContent)entry);
            }

            throw new ArgumentException("BundleContent is not supported", nameof(entry));
        }

        private ProductTileViewModel CreateProductViewModelForEntry(EntryContentBase entry)
        {
            var originalPrice = _pricingService.GetPrice(entry.Code);

            var image = entry.GetAssets<IContentImage>(_contentLoader, _urlResolver).FirstOrDefault() ?? "";

            return new ProductTileViewModel
            {
                Code = entry.Code,
                DisplayName = entry.DisplayName,
                PlacedPrice = originalPrice?.UnitPrice ?? _pricingService.GetMoney(0),
                DiscountedPrice = GetDiscountPrice(entry),
                ImageUrl = image,
                Url = entry.GetUrl(),
                IsAvailable = originalPrice != null
            };
        }

        private ProductTileViewModel CreateProductViewModelForVariant(ProductContent product, VariationContent variant)
        {
            if (variant == null)
            {
                return null;
            }

            var viewModel = CreateProductViewModelForEntry(variant);
            viewModel.Brand = product is FashionProduct ? ((FashionProduct)product).Brand : string.Empty;

            return viewModel;
        }

        private Money? GetDiscountPrice(EntryContentBase entry)
        {
            var originalPrice = _pricingService.GetPrice(entry.Code);

            if (originalPrice != null)
            {
                var discountedPrice = _pricingService.GetDiscountPrice(entry.Code);
                return discountedPrice?.UnitPrice ?? originalPrice.UnitPrice;
            }

            return null;
        }
        public List<ProductTileViewModel> GetBestSellerFasionProduct()
        {
            var result = FindClient.Search<FashionProduct>().OrderByDescending(x => x.Ranking).Skip(0).Take(3);
            var list = result.GetContentResult().ToList();
            var listProductView = list.Select(t => GetProductTileViewModel(t)).ToList();

            return listProductView;
        }
        public List<ProductTileViewModel> GetNewestFasionProduct()
        {
            var result = FindClient.Search<FashionProduct>().OrderByDescending(x => x.StartPublish).Skip(0).Take(3);
            var list = result.GetContentResult().ToList();
            var listProductView = list.Select(t => GetProductTileViewModel(t)).ToList();
            return listProductView;
        }

        public IEnumerable<ProductTileViewModel> GetRelatedProducts(FashionProduct product, int size = 12)
        {
            var query = FindClient.Search<FashionProduct>()
                .Filter(p => p.ParentLink.Match(product.ParentLink))
                .GetContentResult()
                .Items;
            if (size > 0)
                return query.Take(size).Select(s => GetProductTileViewModel(s));
            else
                return query.Select(s => GetProductTileViewModel(s));
        }

        public IEnumerable<ProductTileViewModel> GetMayLikeProducts(FashionProduct product, IEnumerable<ILineItem> lineItems, int size = 12)
        {
            var skuCodes = lineItems.Select(s => s.Code);
            List<ProductTileViewModel> allCartProducts = new List<ProductTileViewModel>();
            skuCodes.ForEach(code =>
            {
                var cartProducts = _relationRepository
                    .GetParents<ProductVariation>(_referenceConverter.GetContentLink(code))
                    .Select(s => GetProductTileViewModel(s.Parent));
                allCartProducts.AddRange(cartProducts);
            });
            var relatedProducts = GetRelatedProducts(product, 0);
            var result = allCartProducts.Intersect(relatedProducts)
                .DistinctBy(d => d.Code)
                .Where(w => w.Code != product.Code).Take(size);
            return result;
        }
    }
}