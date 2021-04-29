using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Navigation.Models
{
    [ContentType(DisplayName = "CopyrightBlock", GUID = "594ad660-c344-40d9-aab9-a8f190fa675d", Description = "")]
    public class CopyrightBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Content",
            Description = "copyright content",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Content { get; set; }
        [Display(
           Name = "NameClass",
           Description = "copyright content display in this <div> tag class css",
           GroupName = SystemTabNames.Content,
           Order = 1)]
        public virtual string Class { get; set; }

    }
}