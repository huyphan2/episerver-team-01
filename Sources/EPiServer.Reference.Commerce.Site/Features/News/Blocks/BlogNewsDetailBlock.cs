using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using EPiServer.SpecializedProperties;
using EPiServer.Reference.Commerce.Site.Features.Checkout.Pages;
using EPiServer.Reference.Commerce.Site.Features.AddressBook.Pages;
using EPiServer.Reference.Commerce.Site.Features.Cart.Pages;
using EPiServer.Reference.Commerce.Site.Features.ResetPassword.Pages;
using EPiServer.Reference.Commerce.Site.Features.Search.Pages;
using System;

namespace EPiServer.Reference.Commerce.Site.Features.News.Blocks
{
    [ContentType(DisplayName = "Blog News Detail", GUID = "A95C010C-0E33-416E-ADC1-D4A2B3641052")]
    public class BlogNewsDetailBlock : BlockData
    {
        [CultureSpecific]
        [Display(
               GroupName = SystemTabNames.Content,
               Order = 1)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(
               Name ="Event Date",
               GroupName = SystemTabNames.Content,
               Order = 10)]
        public virtual DateTime? EventDate { get; set; }

        [CultureSpecific]
        [Display(
             GroupName = SystemTabNames.Content,
             Order = 20)]
        public virtual XhtmlString Description { get; set; }

    }
}