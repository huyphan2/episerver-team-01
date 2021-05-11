using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Order;
using EPiServer.Reference.Commerce.Site.Features.Cart.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModelFactories;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure.Facades;
using EPiServer.Security;
using EPiServer.Web.Mvc;
using Mediachase.Commerce.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.Product.Controllers
{
    public class ProductController : ContentController<FashionProduct>
    {
        private readonly bool _isInEditMode;
        private readonly CatalogEntryViewModelFactory _viewModelFactory;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;

        public ProductController(IsInEditModeAccessor isInEditModeAccessor, CatalogEntryViewModelFactory viewModelFactory, IProductService productService, IOrderRepository orderRepository)
        {
            _isInEditMode = isInEditModeAccessor();
            _viewModelFactory = viewModelFactory;
            _productService = productService;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult Index(FashionProduct currentContent, string entryCode = "", bool useQuickview = false, bool skipTracking = false)
        {
            var viewModel = _viewModelFactory.Create(currentContent, entryCode);
            viewModel.SkipTracking = skipTracking;
            viewModel.RelatedProducts = _productService.GetRelatedProducts(currentContent);
            viewModel.MayLikeProducts = _productService.GetMayLikeProducts(currentContent, GetCurrentLineItems());
            var test = viewModel.MayLikeProducts.Count();
            if (_isInEditMode && viewModel.Variant == null)
            {
                var emptyViewName = "ProductWithoutEntries";
                return Request.IsAjaxRequest() ? PartialView(emptyViewName, viewModel) : (ActionResult)View(emptyViewName, viewModel);
            }

            if (viewModel.Variant == null)
            {
                return HttpNotFound();
            }

            if (useQuickview)
            {
                return PartialView("_Quickview", viewModel);
            }
            return Request.IsAjaxRequest() ? PartialView(viewModel) : (ActionResult)View(viewModel);
        }

        [HttpPost]
        public ActionResult SelectVariant(FashionProduct currentContent, string color, string size, bool useQuickview = false)
        {
            var variant = _viewModelFactory.SelectVariant(currentContent, color, size);
            if (variant != null)
            {
                return RedirectToAction("Index", new { entryCode = variant.Code, useQuickview, skipTracking = true });
            }

            return HttpNotFound();
        }

        private IEnumerable<ILineItem> GetCurrentLineItems()
        {
            var customerId = PrincipalInfo.CurrentPrincipal.GetContactId();
            var cart = _orderRepository.LoadCart<ICart>(customerId, "Default");
            return cart.GetAllLineItems();
        }
    }
}