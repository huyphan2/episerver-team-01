using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Features.Product.Blocks;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.Product.Controllers
{
    public class ListingTileProductBlockController : BlockController<ListingTileProductBlock>
    {
        private IContentLoader _contentLoader;
        private IProductService _productService;
        public ListingTileProductBlockController(IContentLoader contentLoader, IProductService productService)
        {
            _contentLoader = contentLoader;
            _productService = productService;
        }
        public override ActionResult Index(ListingTileProductBlock currentBlock)
        {
            
            ListTileProductViewModel model = new ListTileProductViewModel();
            try
            {
                string contentRefLink = "";
                FashionNode category;
                CatalogContent catalog;
                var haveContentRef  = _contentLoader.TryGet<FashionNode>(currentBlock.ReferContent,out category);
                if (haveContentRef)
                {
                    contentRefLink = category.ContentLink.ToString();
                }
                else
                {
                    haveContentRef = _contentLoader.TryGet<CatalogContent>(currentBlock.ReferContent, out catalog);
                    if (haveContentRef)
                    {
                        contentRefLink = catalog.ContentLink.ToString();
                    }
                }
                var listProduct = _contentLoader.GetDescendents(currentBlock.ReferContent);
                model.productTileViewModels = _productService.GetFasionProductByCategoryAndSorting(contentRefLink, currentBlock.OrderByAttribute, currentBlock.NumberOfItem);
                model.Title = currentBlock.Title;
            }
            catch(Exception ex)
            {
                
                return PartialView(null);
            }
            
            return PartialView(model);
        }
    }
}
