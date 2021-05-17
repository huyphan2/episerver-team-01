using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.Models
{
    public class OrderParam
    {
        public OrderParam() { }
        public OrderParam(int pageNumber)
        {
            this.pageNumber = pageNumber;
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

    }
}