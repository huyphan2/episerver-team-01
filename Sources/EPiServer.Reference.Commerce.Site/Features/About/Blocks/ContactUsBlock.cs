using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.ServiceLocation;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.About.Blocks
{
    [ServiceConfiguration(typeof(IFormContainerBlock))]
    [ContentType(DisplayName = "ContactUsBlock", GUID = "1dfefce0-a66d-4e14-abd8-4b3186a4a5c5", Description = "", AvailableInEditMode = false)]
    public class ContactUsBlock : FormContainerBlock
    { 
        [Display(
            Name = "Name",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Name { get; set; }
    
        [Display(
            Name = "Email",
            Description = "Email field's description",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        [EmailAddress]
        public virtual string Email { get; set; }
        [Display(
            Name = "Mobile",
            Description = "Mobile field's description",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        [Phone]
        public virtual string Mobile { get; set; }
        public virtual string Subject { get; set; }

    }
}