
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Cms.Conventions;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    [ModuleDependency(typeof(InitializationModule))]
    public class EPiServerFindInitialization : IInitializableModule
    {
        private IClient _client;
        private IContentLoader _contentLoader = null;
        public void Initialize(InitializationEngine context)
        {
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            _client = EpiserverFind.Instance.Create();
            ContentIndexer.Instance.Conventions.ForInstancesOf<FashionProduct>().ShouldIndex(x => true);

            var events = context.Locate.ContentEvents();
            events.PublishedContent += Events_PublishedContent;
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
                var product = _contentLoader.Get<FashionProduct>(e.ContentLink);
                product.PublishedDate = DateTime.Now;
               var res =  _client.Index(product);
            }
        }
        public void Uninitialize(InitializationEngine context)
        {
            
        }
    }
}