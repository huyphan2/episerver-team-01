using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.Product.ViewModels
{
    public class ListTileProductViewModel
    {
        public string Title { get; set; }
        public List<ProductTileViewModel> productTileViewModels { get; set; }
    }
}