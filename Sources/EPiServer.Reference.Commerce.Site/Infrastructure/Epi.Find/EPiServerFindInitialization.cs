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

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find
{
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class EPiServerFindInitialization : IInitializableModule
    {
        private IContentLoader _contentLoader = null;
        private IContentRepository _contentRepository = null;        
        //private IProductIndexingService _productIndexingService;
        private IProductService _productService;

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
            _productService= ServiceLocator.Current.GetInstance<IProductService>();
            _client = EpiserverFind.Instance.Create();

            //_client.Conventions.ForInstancesOf<ElectroluxProduct>().ExcludeFieldMatching(x => x.HasMemberAttribute.Equals(nameof(ElectroluxProduct.SeoInformation)));

            var events = context.Locate.ContentEvents();
            events.PublishedContent += Events_PublishedContent;
            //
            ContentIndexer.Instance.Conventions.ForInstancesOf<FashionProduct>().ShouldIndex(x=> true);
           
            //_client.Conventions.NestedConventions.ForInstancesOf<PartProduct>().Add(p => p.AccessoryCategoryNames);                      
            //_client.Conventions.ForInstancesOf<PartProduct>().IncludeField(p => p.PartDetailedDescription.ToInternalString());            
                      

        }

        public void Uninitialize(InitializationEngine context)
        {
            var events = context.Locate.ContentEvents();
            //events.PublishedContent -= Events_PublishedContent;
        }

        private void Events_PublishedContent(object sender, ContentEventArgs e)
        {

            var saveEvent = e as SaveContentEventArgs;
            if (saveEvent != null)
            {
                if (saveEvent.Action == (EPiServer.DataAccess.SaveAction.ForceCurrentVersion | EPiServer.DataAccess.SaveAction.Publish))
                {
                    return;
                }
            }

            if (e.Content is FashionProduct)
            {
                if (_contentLoader == null)
                    _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
                FashionProduct product = (FashionProduct)e.Content;
                product.Price = _productService.GetProductTileViewModel(product).PlacedPrice;
                _client.Index(product);                
            }
        }
    }
}