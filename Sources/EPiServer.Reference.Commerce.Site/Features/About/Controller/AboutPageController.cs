using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.About.Blocks;
using EPiServer.Reference.Commerce.Site.Features.About.Pages;
using EPiServer.Reference.Commerce.Site.Features.About.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure.Attributes;
using EPiServer.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.About.Controller
{
    public class AboutPageController : PageController<AboutPage>
    {
        public ActionResult Index(AboutPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            return View(currentPage);
        }
        [HttpPost]
        [AllowDBWrite]
        public ActionResult Save(ContactUsViewModel viewModel)
        {
            var contentRepo = ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();
             
            return View("Index");
        }
    }
}