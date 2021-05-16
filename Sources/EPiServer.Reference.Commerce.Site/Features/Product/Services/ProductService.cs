using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Api;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.Api.Querying.Filters;
using EPiServer.Find.Api.Querying.Queries;
using EPiServer.Find.Cms;
using EPiServer.Find.Commerce;
using EPiServer.Find.Framework;
using EPiServer.Find.Helpers;
using EPiServer.Globalization;
using EPiServer.Reference.Commerce.Site.Features.Cart.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModelFactories;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Shared.Extensions;
using EPiServer.Reference.Commerce.Site.Features.Shared.Services;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Security;
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
        private readonly IRelationRepository _relationRepository;
        private readonly IContentRepository _contentRepository;
        private readonly ReferenceConverter _referenceConverter;
        private readonly IOrderRepository _orderRepository;
        private readonly LanguageResolver _languageResolver;

        public ProductService(IContentLoader contentLoader,
            IPricingService pricingService,
            UrlResolver urlResolver,
            CatalogEntryViewModelFactory catalogEntryViewModelFactory,
            CatalogContentService catalogContentService,
            IRelationRepository relationRepository,
            ReferenceConverter referenceConverter,
            IContentRepository contentRepository,
            IOrderRepository orderRepository,
            LanguageResolver languageResolver)
        {
            _contentLoader = contentLoader;
            _pricingService = pricingService;
            _urlResolver = urlResolver;
            _catalogContentService = catalogContentService;
            _catalogEntryViewModelFactory = catalogEntryViewModelFactory;
            _relationRepository = relationRepository;
            _referenceConverter = referenceConverter;
            _contentRepository = contentRepository;
            _orderRepository = orderRepository;
            _languageResolver = languageResolver;
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

        private FilterBuilder<FashionProduct> GetRelatedProductQuery(FashionProduct product)
        {
            product.ListCategories = ConvertCategories(product.GetCategories());
            var query = FindClient.BuildFilter<FashionProduct>()
               .FilterOnCurrentMarket()
               .And(p => !p.Code.Match(product.Code));

            if (!Equals(product.ListCategories, null))
            {
                for (int i = 0; i < product.ListCategories.Count; i++)
                {
                    var category = product.ListCategories.ElementAt(0);
                    if (i > 0)
                    {
                        query = query.Or(p => p.ListCategories.MatchCaseInsensitive(category));
                    }
                    else
                    {
                        query = query.And(p => p.ListCategories.MatchCaseInsensitive(category));
                    }
                }
            }
            else
            {
                query = query.And(p => p.Code.Match("null"));
            }
            return query;
        }
        private List<string> ConvertCategories(IEnumerable<ContentReference> categories)
        {
            if (categories == null) return null;
            return categories.Select(contentReference => _contentLoader.Get<FashionNode>(contentReference)?.DisplayName)
                .Distinct().ToList();
        }
        public IEnumerable<ProductTileViewModel> GetRelatedProducts(FashionProduct product, int size = 12)
        {
            var query = GetRelatedProductQuery(product);
            var result = FindClient.Search<FashionProduct>()
                .Filter(query)
                .FilterOnLanguages(new string[] { product.Language.Name });

            if (size > 0)
                return result.Take(size).GetContentResult().Items.Select(GetProductTileViewModel);
            else
                return result.GetContentResult().Items.Select(GetProductTileViewModel);
        }
        public IEnumerable<ProductTileViewModel> GetMayLikeProducts(FashionProduct product, int size = 12)
        {
            var lineItems = GetCurrentLineItems();
            var skuCodes = lineItems.Select(s => s.Code);
            IEnumerable<ProductTileViewModel> result = new List<ProductTileViewModel>();

            if (skuCodes.Any())
            {
                var query = FindClient.BuildFilter<FashionProduct>();
                var resultFind = FindClient
                    .Search<FashionProduct>()
                    .FilterOnLanguages(new string[] { product.Language.Name })
                    .Filter(p => !p.Code.Match(product.Code));

                var cartProducts = _contentRepository.GetItems(
                     skuCodes
                         .Select(x => _referenceConverter.GetContentLink(x))
                         .Where(r => !ContentReference.IsNullOrEmpty(r)), _languageResolver.GetPreferredCulture()).OfType<FashionVariant>()
                         .Select(s => _contentLoader.Get<FashionProduct>(s.GetParentProducts().FirstOrDefault())
                 );

                for (int i = 0; i < cartProducts.Count(); i++)
                {
                    var cartProduct = cartProducts.ElementAt(i);
                    var relatedQuery = GetRelatedProductQuery(cartProduct);
                    if (i > 0)
                    {
                        resultFind = resultFind.OrFilter(relatedQuery);
                    }
                    else
                    {
                        resultFind = resultFind.Filter(relatedQuery);
                    }
                }

                result = resultFind
                    .OrderByDescending(o => o.Ranking)
                    .GetContentResult().Items
                    .Select(GetProductTileViewModel);
            }

            return result;
        }
        public List<ProductTileViewModel> GetFasionProductByCategoryAndSorting(string language, string category, string orderField, int numberOfItem)
        {

            var search = FindClient.Search<FashionProduct>();
            var requiredFilter = new FilterBuilder<FashionProduct>(search.Client);
            requiredFilter = requiredFilter.FilterOnCurrentMarket().And(x => x.Language.Name.MatchCaseInsensitive(language));

            if (!string.IsNullOrEmpty(category))
            {
                requiredFilter = requiredFilter.And(x => x.Ancestors().Match(category));
            }
            var list = new List<FashionProduct>();
            switch (orderField)
            {
                case nameof(FashionProduct.StartPublish):
                    list = search.Filter(requiredFilter).OrderByDescending(t => t.StartPublish).Skip(0).Take(numberOfItem).GetContentResult().ToList();
                    break;
                case nameof(FashionProduct.Ranking):
                    list = search.Filter(requiredFilter).OrderByDescending(t => t.Ranking).Skip(0).Take(numberOfItem).GetContentResult().ToList();
                    break;
                default:
                    list = search.Filter(requiredFilter).OrderByDescending(t => t.Ranking).Skip(0).Take(numberOfItem).GetContentResult().ToList();
                    break;
            }

            var listProductView = list.Select(t => GetProductTileViewModel(t)).ToList();
            return listProductView;

        }


        private IEnumerable<ILineItem> GetCurrentLineItems()
        {
            var customerId = PrincipalInfo.CurrentPrincipal.GetContactId();
            var cart = _orderRepository.LoadCart<ICart>(customerId, "Default");
            return cart?.GetAllLineItems() ?? new List<ILineItem>();
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