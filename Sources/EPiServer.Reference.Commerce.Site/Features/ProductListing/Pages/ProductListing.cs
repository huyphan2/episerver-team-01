using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Shared.Blocks;
using EPiServer.Reference.Commerce.Site.Features.Start.Pages;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using System.ComponentModel.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Start.Models;
using EPiServer.Web;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Pages
{
    [ContentType(DisplayName = "ProductListing", GUID = "f134c5f6-3997-4526-ac93-dcf52c8f9e7f", Description = "")]
    [AvailableContentTypes(IncludeOn = new []{typeof(StartPage)})]
    public class ProductListing : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "Product Content",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SiteTabs.Products,
            Order = 1)]
        [AllowedTypes(typeof(BannerBlock))]
        public virtual ContentArea ProductListingContent { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Price Filter",
            Description = "List Price Filter Param",
            GroupName = SiteTabs.Products,
            Order = 2)]
        
        public virtual IList<double> PriceFilter { get; set; }
        [Ignore]
        public FilterParams FilterParams { get; set; }
        [Ignore]
        public List<ProductTileViewModel> Products { get; set; }
        [Ignore]
        public FilterParam SelectedParams { get; set; }
    }
}