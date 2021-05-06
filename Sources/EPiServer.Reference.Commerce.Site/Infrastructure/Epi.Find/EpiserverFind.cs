using EPiServer.Find;
using EPiServer.Find.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find
{
    public class EpiserverFind
    {
        private static readonly EpiserverFind FindInstance = new EpiserverFind();
        private static readonly IClient Client = SearchClient.Instance;
        static EpiserverFind() { }
        private EpiserverFind() { }
        public static EpiserverFind Instance { get { return FindInstance; } }
        public IClient Create()
        {
            return Client;
        }
    }
}