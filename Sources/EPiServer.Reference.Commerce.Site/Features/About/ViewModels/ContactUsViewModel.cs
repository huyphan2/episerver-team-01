using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.About.ViewModels
{
    public class ContactUsViewModel
    {
        [Display(
            Name = "Name",
            Description = "Name field's description", 
            Order = 1)]
        public virtual string Name { get; set; }

        [Display(
            Name = "Email",
            Description = "Email field's description", 
            Order = 2)]
        [EmailAddress]
        public virtual string Email { get; set; }
        [Display(
            Name = "Mobile",
            Description = "Mobile field's description", 
            Order = 3)]
        [Phone]
        public virtual string Mobile { get; set; }
        public virtual string Subject { get; set; }
    }
}