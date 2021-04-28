using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Shared.Blocks;
using EPiServer.Reference.Commerce.Site.Features.Start.Pages;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

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
        [AllowedTypes(typeof(ProductBanner))]
        public virtual ContentArea ProductListingContent { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Description",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SiteTabs.Products,
            Order = 2)]
        [UIHint(UIHint.Textarea)]
        public virtual string Description { get; set; }
    }
}