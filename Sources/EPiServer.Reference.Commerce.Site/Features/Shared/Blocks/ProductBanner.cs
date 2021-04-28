using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using EPiServer.Web;

namespace EPiServer.Reference.Commerce.Site.Features.Shared.Blocks
{
    [ContentType(DisplayName = "ProductBanner", GUID = "8fff1015-8252-4f74-a22b-694a9e95573a", Description = "")]
    public class ProductBanner : BlockData
    {
        [CultureSpecific]
        [Display(
            Name = "LargeBackgroundImg",
            GroupName = SiteTabs.CarouselBanner,
            Order = 1)]
        [UIHint(UIHints.Images)]
        public virtual ContentReference LargeBackgroundImg{ get; set; }
        [CultureSpecific]
        [Display(
            Name = "MediumBackgroundImg",
            GroupName = SiteTabs.CarouselBanner,
            Order = 2)]
        [UIHint(UIHints.Images)]
        public virtual ContentReference MediumBackgroundImg { get; set; }
        [CultureSpecific]
        [Display(
            Name = "SmallBackgroundImg",
            GroupName = SiteTabs.CarouselBanner,
            Order = 3)]
        [UIHint(UIHints.Images)]
        public virtual ContentReference SmallBackgroundImg { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Price",
            Description = "The Price",
            GroupName = SiteTabs.CarouselBanner,
            Order = 4)]
        public virtual decimal Price { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Title",
            Description = "Should be alphabet",
            GroupName = SiteTabs.CarouselBanner,
            Order = 5)]
        public virtual string Title { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Description",
            Description = "Short banner description",
            GroupName = SiteTabs.CarouselBanner,
            Order = 6)]
        public virtual string Description { get; set; }

    }
}