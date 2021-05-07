using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Api.Querying.Queries;
using EPiServer.Find.Cms;
using EPiServer.Find.Commerce;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using EPiServer.ServiceLocation;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find
{
    [ServiceConfiguration(ServiceType = typeof(IEpiserverFindService))]

    public class EpiserverFindService : FilterBase, IEpiserverFindService
    {
        public ITypeSearch<T> CreateTypeSearch<T>() where T : IContent
        {
            return FindClient.Search<T>();
        }

        public IClient EpiClient()
        {
            return FindClient;
        }

        public IContentResult<FashionVariant> GetFashionCurrentMarket()
        {
            return FindClient.Search<FashionVariant>().FilterOnCurrentMarket()
                .GetContentResult();
        }
    }
}