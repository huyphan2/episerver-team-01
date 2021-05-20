using EPiServer.Commerce.Order;
using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.Reference.Commerce.Site.Features.AddressBook.Services;
using EPiServer.Reference.Commerce.Site.Features.Market.Services;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Models;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Pages;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Shared.Models;
using EPiServer.Reference.Commerce.Site.Features.Shared.Services;
using EPiServer.Reference.Commerce.Site.Infrastructure.Facades;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.Services
{

    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly CustomerContextFacade _customerContext;
        private readonly IAddressBookService _addressBookService;
        private readonly IOrderRepository _orderRepository;
        private readonly ReferenceConverter _referenceConverter;
        private readonly IContentRepository _contentRepository;
        private readonly IPricingService _pricingService;
        public OrderHistoryService(
            CustomerContextFacade customerContextFacade,
            IAddressBookService addressBookService,
            IPricingService pricingService,
            IOrderRepository orderRepository,
            ReferenceConverter referenceConverter,
            IContentRepository contentRepository)
        {
            _customerContext = customerContextFacade;
            _addressBookService = addressBookService;
            _orderRepository = orderRepository;
            _referenceConverter = referenceConverter;
            _contentRepository = contentRepository;
            _pricingService = pricingService;
        }
        public OrderHistoryViewModel GetModel(OrderParam param)
        {
            return GetBaseModel(new OrderHistoryViewModel(param));
        }
        public OrderHistoryViewModel GetModel(OrderHistoryPage page)
        {
            return GetBaseModel(new OrderHistoryViewModel { CurrentPage = page });
        }

        public OrderHistoryViewModel GetModelByOrderNumber(string orderNumber)
        {
            var model = GetBaseModel(new OrderHistoryViewModel(new OrderParam().BuildOrderNumber(orderNumber)));
            return model;
        }

        private OrderHistoryViewModel GetBaseModel(OrderHistoryViewModel baseModel)
        {

            baseModel.BindingParam();
            var dataLoad = _orderRepository.Load<IPurchaseOrder>(_customerContext.CurrentContactId);

            if (!Equals(baseModel.SearchParam, null))
            {
                var term = baseModel.SearchParam.term;
                var orderNumber = baseModel.SearchParam.orderNumber;
                if (!string.IsNullOrWhiteSpace(term))
                {
                    dataLoad = dataLoad
                       .Where(w =>
                           w.GetAllLineItems().Any(item => item.DisplayName.Contains(term))
                         || w.GetAllLineItems().Any(item => item.Code.Contains(term))
                         || w.OrderNumber.Contains(term)
                       );
                }
                if (!string.IsNullOrWhiteSpace(orderNumber))
                {
                    dataLoad = dataLoad.Where(w => w.OrderNumber == orderNumber);
                }
            }
            var purchaseOrders =
                dataLoad
                .Skip(baseModel.Skip)
                .Take(baseModel.Take)
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
                        Variant = _contentRepository.Get<FashionVariant>(_referenceConverter.GetContentLink(lineItem.Code))
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
            baseModel.PricingService = _pricingService;
            return baseModel;
        }
    }
}