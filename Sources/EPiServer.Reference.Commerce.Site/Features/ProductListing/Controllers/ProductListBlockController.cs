using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Blocks;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Controllers
{
    public class ProductListBlockController : BlockController<ProductListBlock>
    {
        private readonly IProductListingService _productListingService;

        public ProductListBlockController(IProductListingService productListingService)
        {
            _productListingService = productListingService;
        }

        public override ActionResult Index(ProductListBlock currentBlock)
        {
            //get data from pagecontroller.
            var from = ControllerContext.ParentActionViewContext.ViewData["priceFrom"];
            var to = ControllerContext.ParentActionViewContext.ViewData["priceTo"];
            var Number = ControllerContext.ParentActionViewContext.ViewData["pageNumber"];

            var brand = ControllerContext.ParentActionViewContext.ViewData["brand"]?.ToString() ?? string.Empty;
            var category = ControllerContext.ParentActionViewContext.ViewData["category"]?.ToString() ?? string.Empty;
            var priceFrom = from != null ? double.Parse(from.ToString()) : 0;
            var priceTo = to != null ? double.Parse(to.ToString()) : 0;
            var isSortDes = ControllerContext.ParentActionViewContext.ViewData["isSortDes"] != null ? (bool)ControllerContext.ParentActionViewContext.ViewData["isSortDes"] : false;
            var pageNumber = Number != null ? int.Parse(Number.ToString()) : 1;
            //
            var currentLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            var products = _productListingService.GetListProduct(brand, (decimal)priceFrom, (decimal)priceTo, category, isSortDes, pageNumber, currentLanguage);
            currentBlock.Products = products != null && products.Products != null ? products.Products : new List<ProductTileViewModel>();
            currentBlock.FilterParams = _productListingService.GetFilterParams(currentBlock);
            currentBlock.SelectedParams = new FilterParam()
            {
                Brand = brand,
                Category = category,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
            };
            return PartialView(currentBlock);
        }
    }
}