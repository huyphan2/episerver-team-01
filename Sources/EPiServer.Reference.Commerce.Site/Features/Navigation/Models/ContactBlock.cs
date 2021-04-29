using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Navigation.Models
{
    [ContentType(DisplayName = "ContactBlock", GUID = "9874629b-d5b9-4a0c-ae19-00cc348a7860", Description = "")]
    public class ContactBlock : BlockData
    {
         public virtual ContentArea LinkItems { get; set; }
    }
}