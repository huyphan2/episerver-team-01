using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Navigation.Models
{
    [ContentType(DisplayName = "IconLinkBlock", GUID = "6d360e3f-ca0d-468b-b90f-45aec1820c2a", Description = "")]
    public class IconLinkBlock : BlockData
    {
 
        [Display(
           Name = "Class",
           Description = "class of link",
           GroupName = SystemTabNames.Content,
           Order = 0)] 
        public virtual  string Class { get; set; }
        [Display(
          Name = "IconClass",
          Description = "class of icon",
          GroupName = SystemTabNames.Content,
          Order = 0)]
        public virtual string IconClass { get; set; }
        [Display(
          Name = "Url",
          Description = "Url of link",
          GroupName = SystemTabNames.Content,
          Order = 0)]
        public virtual Url Url { get; set; }
    }
}