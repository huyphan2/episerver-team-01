using EPiServer.Commerce.Order;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.ViewModels
{
    public class OrderHistoryItemViewModel
    {
        public ILineItem LineItem { get; set; }
        public FashionVariant Variant { get; set; }
    }
}