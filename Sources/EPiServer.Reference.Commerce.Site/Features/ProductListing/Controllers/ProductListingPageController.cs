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
    public class ProductListingPageController : PageController<Pages.ProductListingPage>
    {
        private readonly IProductListingService _productListingService;

        public ProductListingPageController(IProductListingService productListingService)
        {
            _productListingService = productListingService;
        }

        public ActionResult Index(Pages.ProductListingPage currentPage, string brand, string category, double priceFrom = 0, double priceTo = 0, bool isSortDes = false, int pageNumber = 1)
        {
            currentPage.SelectedParams = new SearchParamPageAtrribute()
            {
                Category = category,
                Brand = brand,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                IsSortDes = isSortDes,
                PageNumber = pageNumber
            };
            return View(currentPage);
        }
        //public ActionResult Index(Pages.ProductListingPage currentPage,string brand,string category, double priceFrom = 0, double priceTo = 0, bool isSortDes=false,int pageNumber=1)
        //{
        //    var products = _productListingService.GetListProduct(brand, (decimal)priceFrom, (decimal)priceTo, category, isSortDes, pageNumber);
        //    currentPage.Products = products != null && products.Products != null ? products.Products : new List<ProductTileViewModel>();
        //    currentPage.FilterParams = _productListingService.GetFilterParams(currentPage);
        //    currentPage.SelectedParams = new FilterParam()
        //    {
        //        Brand = brand,
        //        Category = category,
        //        PriceFrom = priceFrom,
        //        PriceTo = priceTo,
        //    };
        //    return View(currentPage);
        //}
    }
}