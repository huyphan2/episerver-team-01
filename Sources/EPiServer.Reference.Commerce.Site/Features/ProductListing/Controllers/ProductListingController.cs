using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Controllers
{
    public class ProductListingController : PageController<Pages.ProductListing>
    {
        public ActionResult Index(Pages.ProductListing currentPage)
        {

            return View(currentPage);
        }
    }
}