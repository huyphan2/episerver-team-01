using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Navigation.Models
{
    [ContentType(DisplayName = "AboutUsBlock", GUID = "e9267195-073d-4c1e-986e-bcd15ee3d2f6", Description = "")]
    public class AboutUsBlock : BlockData
    {
        public virtual string Title { get; set; }
        public virtual XhtmlString Content { get; set; }
    }
}