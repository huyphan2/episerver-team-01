using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Web;
using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using EPiServer.Reference.Commerce.Site.Infrastructure.Validation;
using EPiServer.SpecializedProperties;

namespace EPiServer.Reference.Commerce.Site.Features.Start.Models
{
    [ContentType(DisplayName = "BannerItemBlock", GUID = "71ba1d2b-4c94-4efd-8518-3442f729f3bd", Description = "")]
    public class BannerItemBlock : BlockData
    {
        [CultureSpecific]
        [Display(
         Name = "Title",
         Description = "Title of banner item, display in <h2> tag",
         GroupName = SiteTabs.CarouselBanner,
         Order = 1)]
        public virtual string Title { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Description",
            Description = "Description of banner item",
            GroupName = SiteTabs.CarouselBanner,
            Order = 2)]
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual string Note { get; set; }
        [LinkItemCollectionMaxItemsCount(1,ErrorMessage = "Max 1 item")]
        public virtual LinkItemCollection Link { get; set; }
        [Ignore]
        public virtual Url DirectLink { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Large Background",
            GroupName = SiteTabs.CarouselBanner,
            Order = 1)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference LargeBackground { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Medium Background",
            GroupName = SiteTabs.CarouselBanner,
            Order = 2)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MediumBackground { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Small Background",
            GroupName = SiteTabs.CarouselBanner,
            Order = 3)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference SmallBackground { get; set; }

        [AllowedTypes(AllowedTypes = new[] { typeof(FashionProduct) })]
        public virtual ContentReference Product { get; set; }
    }
}