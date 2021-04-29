
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    public class SiteViewEngine : RazorViewEngine
    {
        private static readonly string[] AdditionalPartialViewFormats = new[]
            {
                ViewTemplateModelRegistrator.BlockFolder + "{0}.cshtml",
                ViewTemplateModelRegistrator.PagePartialsFolder + "{0}.cshtml"
            };

        public SiteViewEngine()
        {
       
            PartialViewLocationFormats = PartialViewLocationFormats.Union(AdditionalPartialViewFormats).ToArray();
        }
    }
}