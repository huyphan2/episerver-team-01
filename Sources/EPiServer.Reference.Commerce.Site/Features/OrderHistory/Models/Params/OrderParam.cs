using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.OrderHistory.Models
{
    public class OrderParam
    {
        public OrderParam()
        {
            this.pageNumber = 1;
            this.pageSize = 6;
        }
        public OrderParam(int pageNumber, int pageSize = 6)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
        public OrderParam BuildTerm(string term)
        {
            this.term = term;
            return this;
        }
        public OrderParam BuildOrderNumber(string orderNumber)
        {
            this.orderNumber = orderNumber;
            return this;
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string term { get; set; }

        public string orderNumber { get; set; }
    }
}