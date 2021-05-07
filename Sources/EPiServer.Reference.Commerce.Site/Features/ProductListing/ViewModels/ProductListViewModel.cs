using System.Collections.Generic;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using Newtonsoft.Json;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            Products = new List<ProductTileViewModel>();
        }
        public int TotalProducts { get; set; }
        public int PageSize{ get; set; }
        public List<ProductTileViewModel> Products { get; set; }
    }

    public class ProductListResponse
    {
        public string Html { get; set; }

        public bool HasMore { get; set; }
    }
}