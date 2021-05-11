using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.About.Blocks
{
    [ContentType(DisplayName = "FindUsBlock", GUID = "ee31a0db-f5a8-49c2-aa70-866cd94eff70", Description = "")]
    public class FindUsBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Content",
            Description = "Content display",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentArea Content { get; set; }

    }
}