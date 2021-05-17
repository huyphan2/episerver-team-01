using EPiServer.Commerce.Order;
using EPiServer.Reference.Commerce.Site.Features.AddressBook.Services;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Models;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Pages;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Shared.Models;
using EPiServer.Reference.Commerce.Site.Infrastructure.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.Services
{

    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly CustomerContextFacade _customerContext;
        private readonly IAddressBookService _addressBookService;
        private readonly IOrderRepository _orderRepository;
        public OrderHistoryService(CustomerContextFacade customerContextFacade, IAddressBookService addressBookService, IOrderRepository orderRepository)
        {
            _customerContext = customerContextFacade;
            _addressBookService = addressBookService;
            _orderRepository = orderRepository;
        }
        public OrderHistoryViewModel GetModel(OrderParam param)
        {
            return GetBaseModel(new OrderHistoryViewModel());
        }
        public OrderHistoryViewModel GetModel(OrderHistoryPage page)
        {
            return GetBaseModel(new OrderHistoryViewModel { CurrentPage = page });
        }

        private OrderHistoryViewModel GetBaseModel(OrderHistoryViewModel baseModel)
        {
            var purchaseOrders = _orderRepository
                .Load<IPurchaseOrder>(_customerContext.CurrentContactId)
                .Take(baseModel.PageSize)
                .OrderByDescending(x => x.Created)
                .ToList();
            baseModel.Orders = new List<OrderViewModel>();
            foreach (var purchaseOrder in purchaseOrders)
            {
                // Assume there is only one form per purchase.
                var form = purchaseOrder.GetFirstForm();
                var billingAddress = new AddressModel();
                var payment = form.Payments.FirstOrDefault();
                if (payment != null)
                {
                    billingAddress = _addressBookService.ConvertToModel(payment.BillingAddress);
                }
                var orderViewModel = new OrderViewModel
                {
                    PurchaseOrder = purchaseOrder,
                    Items = form.GetAllLineItems().Select(lineItem => new OrderHistoryItemViewModel
                    {
                        LineItem = lineItem,
                    }).GroupBy(x => x.LineItem.Code).Select(group => group.First()),
                    BillingAddress = billingAddress,
                    ShippingAddresses = new List<AddressModel>()
                };

                foreach (var orderAddress in purchaseOrder.Forms.SelectMany(x => x.Shipments).Select(s => s.ShippingAddress))
                {
                    var shippingAddress = _addressBookService.ConvertToModel(orderAddress);
                    orderViewModel.ShippingAddresses.Add(shippingAddress);
                }

                baseModel.Orders.Add(orderViewModel);
            }
            return baseModel;
        }
    }
}