using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Models;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.Pages;
using EPiServer.Reference.Commerce.Site.Features.OrderHistory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.Services
{
    public interface IOrderHistoryService
    {
        OrderHistoryViewModel GetModel(OrderParam param);
        OrderHistoryViewModel GetModel(OrderHistoryPage page);
        OrderHistoryViewModel GetModelByOrderNumber(string orderNumber);
    }
}