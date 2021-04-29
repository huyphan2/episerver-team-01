using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Navigation.Models
{
    [ContentType(DisplayName = "FooterBlock", GUID = "f15d2637-9c18-4b50-942d-cbe0f7342494", Description = "")]
    public class FooterBlock : BlockData
    {
        [CultureSpecific]
        [Display(
           Name = "Content",
           Description = "Content of footer page",
           GroupName = SystemTabNames.Content,
           Order = 2)]
        [AllowedTypes(new[] {typeof(AboutUsBlock), typeof(CopyrightBlock) , typeof(ContactBlock) })]
        public virtual ContentArea Content { get; set; }
    }
}