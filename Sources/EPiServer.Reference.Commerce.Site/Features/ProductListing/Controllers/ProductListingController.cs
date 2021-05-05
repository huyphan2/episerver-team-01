using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
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
            var products = _productListingService.GetListProduct("", 0, "Mens Shoes");
            currentPage.Products = products != null? products.ToList():new List<ProductTileViewModel>();
            currentPage.FilterParams = _productListingService.GetFilterParams(currentPage);
            return View(currentPage);
        }
    }
}