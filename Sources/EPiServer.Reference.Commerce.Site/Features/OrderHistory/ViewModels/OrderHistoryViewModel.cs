using System.Collections.Generic;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Models;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Pages;
using EPiServer.Reference.Commerce.Site.Features.Shared.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.ViewModels
{
    public class OrderHistoryViewModel : PageViewModel<OrderHistoryPage>
    {
        public OrderHistoryViewModel()
        {
        }
        public OrderHistoryViewModel(OrderParam orderParam)
        {
            SearchParam = orderParam;
        }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 6;
        public int Total { get; set; }

        public OrderHistoryViewModel BindingParam()
        {
            if (!Equals(SearchParam, null))
            {
                Take = SearchParam.pageSize;
                Skip = ((SearchParam.pageNumber < 1 ? 1 : SearchParam.pageNumber) - 1) * Take;
            }
            return this;
        }

        public OrderParam SearchParam { get; set; }

        public List<OrderViewModel> Orders { get; set; }
    }
}