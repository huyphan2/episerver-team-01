using System.Collections.Generic;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels
{
    public class FilterParams
    {
        public List<string> Brands { get; set; }
        public List<string> Categories { get; set; }
        public List<decimal> Price{ get; set; }
    }
}