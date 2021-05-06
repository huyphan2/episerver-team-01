using System;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure;

namespace EPiServer.Reference.Commerce.Site.WebApi
{
    [RoutePrefix("api/productlist")]
    public class ProductListApiController : ApiController
    {
        private IProductListingService _productListingService;

        public ProductListApiController(IProductListingService productListingService)
        {
            _productListingService = productListingService;
        }
        [HttpGet]
        [Route("GetProductList")]
        public IHttpActionResult GetProductList(string brand, string category, decimal price, bool isSortDes = false, int pageNumber = 1)
        {
            try
            {
                var productlist = _productListingService.GetListProduct(brand, price, category, isSortDes, pageNumber);
                var viewrenderer = new ViewRenderer();
                var html = viewrenderer.RenderPartialViewToString("~/Views/Shared/_ProductList.cshtml", productlist.Products);
                var response = new ProductListResponse()
                {
                    Html = html,
                    HasMore = productlist.Products.Any()
                };
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        //[HttpGet]
        //[Route("GetProductName")]
        //public IHttpActionResult GetProductName(string text)
        //{
        //    try
        //    {
        //        var productlist = _productListingService.GetListProduct(brand, price, category, isSortDes, pageNumber);
        //        var viewrenderer = new ViewRenderer();
        //        var html = viewrenderer.RenderPartialViewToString("~/Views/Shared/_ProductList.cshtml", productlist.Products);
        //        var response = new ProductListResponse()
        //        {
        //            Html = html,
        //            HasMore = productlist.Products.Any()
        //        };
        //        return Ok(response);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
