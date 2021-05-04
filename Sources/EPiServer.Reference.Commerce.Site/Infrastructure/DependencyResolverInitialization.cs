//using EPiServer.Framework;
//using EPiServer.Framework.Initialization;
//using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
//using EPiServer.ServiceLocation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;

//namespace EPiServer.Reference.Commerce.Site.Infrastructure
//{
//    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
//    public class DependencyResolverInitialization : IConfigurableModule
//    {
//        public void ConfigureContainer(ServiceConfigurationContext context)
//        {
//            context.Services.AddTransient<EpiserverFindService>();
//            context.Services.AddTransient<IProductListingService, ProductListingService>();
//        }

//        public void Initialize(InitializationEngine context)
//        {            
//        }

//        public void Uninitialize(InitializationEngine context)
//        {            
//        }
//    }
//}