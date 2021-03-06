using EPiServer.Core;
using EPiServer.Personalization.Commerce.Tracking;
using EPiServer.Tracking.Commerce;
using EPiServer.Reference.Commerce.Site.Features.Start.Pages;
using System.Collections.Generic;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.Start.ViewModels
{
    public class StartPageViewModel
    {
        public StartPage StartPage { get; set; }
        public IEnumerable<PromotionViewModel> Promotions { get; set; }
        public IEnumerable<Recommendation> Recommendations { get; set; }
        public IEnumerable<ProductTileViewModel> BestSeller { get; set; }
        public IEnumerable<ProductTileViewModel> NewestProduct { get; set; }
         
    }
}