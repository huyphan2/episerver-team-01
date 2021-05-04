using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Start.Models
{
    [ContentType(DisplayName = "BannerBlock", GUID = "e40d99c5-eaae-4596-9a76-cfbc85c64a57", Description = "")]
    public class BannerBlock : BlockData
    {
        [AllowedTypes(new[] {typeof(BannerItemBlock) })]
         public virtual ContentArea BannerItems { get; set; }
    }
}