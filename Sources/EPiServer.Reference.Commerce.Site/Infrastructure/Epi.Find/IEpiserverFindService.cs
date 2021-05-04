using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find
{
    public interface IEpiserverFindService
    {
        ITypeSearch<T> CreateTypeSearch<T>() where  T: IContent;
        IClient EpiClient();
        IContentResult<FashionVariant> GetFashionCurrentMarket();
    }
}