using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using EPiServer.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Controllers
{
    public class ProductListingController : PageController<Pages.ProductListing>
    {
        private IProductListingService _productListingService;

        public ProductListingController(IProductListingService productListingService)
        {
            _productListingService = productListingService;
        }

        public ActionResult Index(Pages.ProductListing currentPage)
        {
            var abc = _productListingService.GetListProduct("Faded Glory huy", "abc", 155);
            return View(currentPage);
        }
    }
}