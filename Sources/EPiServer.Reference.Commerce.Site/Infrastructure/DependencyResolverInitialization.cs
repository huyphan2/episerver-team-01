using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    [InitializableModule]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<EpiserverFindService>();
        }

        public void Initialize(InitializationEngine context)
        {            
        }

        public void Uninitialize(InitializationEngine context)
        {            
        }
    }
}