using EPiServer.Reference.Commerce.Site.Features.AddressBook.Services;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Pages;
using EPiServer.Reference.Commerce.Site.Features.Shared.Models;
using EPiServer.Reference.Commerce.Site.Infrastructure.Facades;
using EPiServer.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Commerce.Order;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Services;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.Controllers
{
    [Authorize]
    public class OrderHistoryController : PageController<OrderHistoryPage>
    {
        private readonly IOrderHistoryService _orderHistoryService;
        public OrderHistoryController(IOrderHistoryService orderHistoryService)
        {
            _orderHistoryService = orderHistoryService;
        }

        [HttpGet]
        public ActionResult Index(OrderHistoryPage currentPage)
        {
            var viewModel = _orderHistoryService.GetModel(currentPage);
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult Detail(string orderNumber)
        {
            var viewModel = _orderHistoryService.GetModelByOrderNumber(orderNumber);
            if (Equals(viewModel.Orders, null) || !viewModel.Orders.Any())
            {
                return RedirectToAction("Forbidden", "ErrorHandling", new { message = "You have not permission to access this page" });
            }
            return View("Detail", viewModel);
        }

    }
}