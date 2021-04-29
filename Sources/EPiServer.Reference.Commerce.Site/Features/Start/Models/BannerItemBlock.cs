using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Start.Models
{
    [ContentType(DisplayName = "BannerItemBlock", GUID = "71ba1d2b-4c94-4efd-8518-3442f729f3bd", Description = "")]
    public class BannerItemBlock : BlockData
    {
        [CultureSpecific]
        [Display(
         Name = "Title",
         Description = "Title of banner item, display in <h2> tag",
         GroupName = SystemTabNames.Content,
         Order = 1)]
        public virtual string Title { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Description",
            Description = "Description of banner item",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual string Note { get; set; }
        public virtual Url DirectLink { get; set; }
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [AllowedTypes(AllowedTypes = new[] { typeof(FashionProduct) })]
        public virtual ContentReference Product { get; set; }
    }
}