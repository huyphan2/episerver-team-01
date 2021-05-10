using System.Collections.Generic;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Models;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels
{
    public class FilterParams
    {
        public List<string> Brands { get; set; }
        public List<string> Categories { get; set; }
        public List<PriceRange> Price{ get; set; }
    }
    public class FilterParam
    {
        public string Brand { get; set; }
        public string Category { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
    }

    public class SearchParamPageAtrribute
    {
        public string Brand { get; set; }
        public string Category { get; set; }
        public double PriceFrom{ get; set; }
        public double PriceTo { get; set; }
        public bool IsSortDes { get; set; }
        public int PageNumber { get; set; }
    }
}