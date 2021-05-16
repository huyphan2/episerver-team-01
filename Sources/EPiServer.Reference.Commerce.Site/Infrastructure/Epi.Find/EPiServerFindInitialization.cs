using EPiServer.Commerce.Initialization;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Cms.Conventions;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.Services;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Find.ClientConventions;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find
{
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class EPiServerFindInitialization : IInitializableModule
    {
        private IContentLoader _contentLoader = null;
        private IContentRepository _contentRepository = null;
        //private IProductIndexingService _productIndexingService;
        private IProductService _productService;
        private IProductListingService _productListingService;
        private IClient _client;


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks>
        /// Gets called as part of the EPiServer Framework initialization sequence. Note that it will be called
        /// only once per AppDomain, unless the method throws an exception. If an exception is thrown, the initialization
        /// method will be called repeadetly for each request reaching the site until the method succeeds.
        /// </remarks>
        public void Initialize(InitializationEngine context)
        {
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            //_productIndexingService = ServiceLocator.Current.GetInstance<IProductIndexingService>();
            _contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            _productService = ServiceLocator.Current.GetInstance<IProductService>();
            _client = EpiserverFind.Instance.Create();
            _productListingService = ServiceLocator.Current.GetInstance<IProductListingService>();

            //_client.Conventions.ForInstancesOf<ElectroluxProduct>().ExcludeFieldMatching(x => x.HasMemberAttribute.Equals(nameof(ElectroluxProduct.SeoInformation)));

            var events = context.Locate.ContentEvents();
            //events.PublishedContent += Events_PublishedContent;
            //
            ContentIndexer.Instance.Conventions.ForInstancesOf<FashionProduct>().ShouldIndex(ShouldIndexFashionProduct);
            _client.Conventions.ForInstancesOf<FashionProduct>().IncludeField(p => p.Price);
            _client.Conventions.ForInstancesOf<FashionProduct>().IncludeField(p => p.ListCategories);

            //_client.Conventions.NestedConventions.ForInstancesOf<PartProduct>().Add(p => p.AccessoryCategoryNames);                      
            //_client.Conventions.ForInstancesOf<PartProduct>().IncludeField(p => p.PartDetailedDescription.ToInternalString());            


        }

        public void Uninitialize(InitializationEngine context)
        {
            var events = context.Locate.ContentEvents();
            //events.PublishedContent -= Events_PublishedContent;
        }

        private bool ShouldIndexFashionProduct(FashionProduct theProduct)
        {
            var product = _productService.GetProductTileViewModel(theProduct);
            if (!Equals(product, null))
            {
                theProduct.Price = _productService.GetProductTileViewModel(theProduct).PlacedPrice;
                theProduct.ListCategories = _productListingService.ProductCategories(theProduct.GetCategories());
            }
            return true;
        }
    }
}