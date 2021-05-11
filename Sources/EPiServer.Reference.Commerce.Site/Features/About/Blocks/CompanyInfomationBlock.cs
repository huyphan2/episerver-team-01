using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.About.Blocks
{
    [ContentType(DisplayName = "CompanyInfomationBlock", GUID = "61b4c25d-09c5-4776-b95f-2eb0631006f5", Description = "")]
    public class CompanyInfomationBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Content",
            Description = "Content display",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString Content { get; set; }

    }
}