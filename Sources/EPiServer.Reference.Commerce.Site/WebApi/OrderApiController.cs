using System;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using EPiServer.Reference.Commerce.Site.Infrastructure.Facades;
using EPiServer.Reference.Commerce.Site.Features.AddressBook.Services;
using EPiServer.Commerce.Order;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Shared.Models;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Services;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Models;

namespace EPiServer.Reference.Commerce.Site.WebApi
{
    [RoutePrefix("api/order")]
    public class OrderApiController : ApiController
    {
        private readonly IOrderHistoryService _orderHistoryService;
        private  IViewRender _viewRender;
        public OrderApiController(IOrderHistoryService orderHistoryService)
        {
            _orderHistoryService = orderHistoryService;
          //  _viewRender = viewRender;
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(int pageNumber = 1)
        {
            try
            {
                var viewModel = _orderHistoryService.GetModel(new OrderParam(pageNumber));
                _viewRender = new ViewRenderer();
                var html = _viewRender.RenderPartialViewToString("~/Views/OrderHistory/DataTable.cshtml", viewModel);
                var response = new
                {
                    Html = html,
                    HasMore = (pageNumber * viewModel.PageSize) < viewModel.Total
                };
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
