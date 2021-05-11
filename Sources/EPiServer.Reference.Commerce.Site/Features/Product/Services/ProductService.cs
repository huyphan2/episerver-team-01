using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Api;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.Cms;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModelFactories;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Shared.Extensions;
using EPiServer.Reference.Commerce.Site.Features.Shared.Services;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public ProductService(IContentLoader contentLoader,
            IPricingService pricingService,
            UrlResolver urlResolver,
            CatalogEntryViewModelFactory catalogEntryViewModelFactory,
            CatalogContentService catalogContentService)
        {
            _contentLoader = contentLoader;
            _pricingService = pricingService;
            _urlResolver = urlResolver;
            _catalogContentService = catalogContentService;
            _catalogEntryViewModelFactory = catalogEntryViewModelFactory;
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
            var result = FindClient.Search<FashionProduct>().OrderByDescending(x => x.Ranking).Skip(0).Take(6);
            var list = result.GetContentResult().ToList();
            var listProductView = list.Select(t => GetProductTileViewModel(t)).ToList();

            return listProductView;
        }
        public List<ProductTileViewModel> GetNewestFasionProduct()
        {
            var result = FindClient.Search<FashionProduct>().OrderByDescending(x => x.StartPublish).Skip(0).Take(6);
            var list = result.GetContentResult().ToList();
            var listProductView = list.Select(t => GetProductTileViewModel(t)).ToList();
            return listProductView;
        }
        public List<ProductTileViewModel> GetFasionProductByCategoryAndSorting(string category, string orderField, int numberOfItem)
        {

            var search = FindClient.Search<FashionProduct>();
            if (!string.IsNullOrEmpty(category))
            {
                search = search.Filter(x => x.Ancestors().Match(category));
            }
            var list = new List<FashionProduct>();
            switch (orderField)
            {
                case nameof(FashionProduct.StartPublish):
                    list = search.OrderByDescending(t => t.StartPublish).Skip(0).Take(numberOfItem).GetContentResult().ToList();
                    break;
                case nameof(FashionProduct.Ranking):
                    list = search.OrderByDescending(t => t.Ranking).Skip(0).Take(numberOfItem).GetContentResult().ToList();
                    break;
                default:
                    list = search.OrderByDescending(t => t.Ranking).Skip(0).Take(numberOfItem).GetContentResult().ToList();
                    break;
            }
            
            var listProductView = list.Select(t => GetProductTileViewModel(t)).ToList();
            return listProductView;

        }
    }

    public static class Extension
    {


        //public static ITypeSearch<TSource> OrderByDynamics<TSource>(this ITypeSearch<TSource> search, string columnName)
        //{
        //    ParameterExpression parameter = Expression.Parameter(typeof(TSource), "d");
        //    MemberExpression memberExpression = Expression.Property(parameter, typeof(TSource).GetProperty(columnName));
        //    LambdaExpression lambda = Expression.Lambda(memberExpression, parameter);
        //    //Expression<Func<TSource, EPiServer.Find.GeoLocation>> lambda =  Expression.Lambda<Func<TSource, EPiServer.Find.GeoLocation>>(memberExpression, parameter);
        //    //var res = search.OrderByDescending(t => Expression.Property(parameter, typeof(TSource).GetProperty(columnName)));
        //    //var res2 = search.OrderByDescending(lambda);

        //    switch (columnName)
        //    {
        //        case nameof(FashionProduct.PublishedDate):
        //            return search.OrderByDescending(t => (t as FashionProduct).PublishedDate); 
        //        case nameof(FashionProduct.PublishedDate):
        //            return search.OrderByDescending(t => (t as FashionProduct).PublishedDate); 
        //        default:
        //            return search;
        //    }

            

        //    //return new Search<TSource, IQuery>(search, context =>
        //    //{
        //    //    context.RequestBody.Sort.Add(new Sorting(columnName));
        //    //});
        //}
    }

}