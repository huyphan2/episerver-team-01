using System.Collections.Generic;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels
{
    public class FilterParams
    {
        public List<string> Brands { get; set; }
        public List<string> Categories { get; set; }
        public List<double> Price{ get; set; }
    }
    public class FilterParam
    {
        public string Brand { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
}