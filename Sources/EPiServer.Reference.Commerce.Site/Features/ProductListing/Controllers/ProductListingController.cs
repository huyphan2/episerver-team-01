using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Controllers
{
    public class ProductListingController : PageController<Pages.ProductListing>
    {
        private readonly IProductListingService _productListingService;

        public ProductListingController(IProductListingService productListingService)
        {
            _productListingService = productListingService;
        }

        public ActionResult Index(Pages.ProductListing currentPage,string brand,string category, double price = 0, bool isSortDes=false,int pageNumber=1)
        {
            var products = _productListingService.GetListProduct(brand, (decimal)price, category,isSortDes,pageNumber);
            currentPage.Products = products != null && products.Products!=null? products.Products :new List<ProductTileViewModel>();
            currentPage.FilterParams = _productListingService.GetFilterParams(currentPage);
            currentPage.SelectedParams = new FilterParam()
            {
                Brand = brand,
                Category = category,
                Price = price
            };
            return View(currentPage);
        }
    }
}