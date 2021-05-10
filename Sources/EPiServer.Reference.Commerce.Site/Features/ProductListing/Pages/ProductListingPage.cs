using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Shared.Blocks;
using EPiServer.Reference.Commerce.Site.Features.Start.Pages;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using System.ComponentModel.DataAnnotations;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Start.Models;
using EPiServer.Web;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Blocks;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Models;
using EPiServer.Shell.ObjectEditing;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Pages
{
    [ContentType(DisplayName = "ProductListing", GUID = "f134c5f6-3997-4526-ac93-dcf52c8f9e7f", Description = "")]
    [AvailableContentTypes(IncludeOn = new []{typeof(StartPage)})]
    public class ProductListingPage : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "Carousel Area",
            GroupName = SiteTabs.Products,
            Order = 1)]
        [AllowedTypes(typeof(BannerBlock))]
        public virtual ContentArea CarouselArea { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Product Content",
            Description = "Include filter navigation and list products",
            GroupName = SiteTabs.Products,
            Order = 2)]
        [AllowedTypes(typeof(ProductListBlock))]
        public virtual ContentArea ProductListing { get; set; }
        [Ignore]
        public SearchParamPageAtrribute SelectedParams { get; set; }
        //[CultureSpecific]
        //[Display(
        //    Name = "Price Filter",
        //    Description = "List price range for searching",
        //    GroupName = SiteTabs.Products,
        //    Order = 2)]
        //[EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<PriceRange>))]
        //public virtual IList<PriceRange> PriceFilter { get; set; }
        //[Ignore]
        //public FilterParams FilterParams { get; set; }
        //[Ignore]
        //public List<ProductTileViewModel> Products { get; set; }
        //[Ignore]
        //public FilterParam SelectedParams { get; set; }
    }
}