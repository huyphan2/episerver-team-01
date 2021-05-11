using EPiServer.Reference.Commerce.Site.Features.About.Blocks;
using EPiServer.Reference.Commerce.Site.Features.About.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.About.ViewModels
{
    public class AfterSubmissionViewModel
    {
        public AfterSubmissionPage CurrentPage { get; set; }
        public ContactUsBlock ContactUsBlock { get; set; }
    }
}