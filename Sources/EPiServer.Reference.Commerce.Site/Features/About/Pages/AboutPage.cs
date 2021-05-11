using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.About.Blocks;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.About.Pages
{
    [ContentType(DisplayName = "AboutPage", GUID = "40134ccb-ae29-46dc-a54c-9b9b2885c1ac", Description = "")]
    public class AboutPage : PageData
    {
        public virtual ContentArea Content { get; set; } 
        public virtual ContentArea ContactUsForm { get; set; }
    }
}