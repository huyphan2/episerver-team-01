using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EPiServer.Reference.Commerce.Site.WebApi
{
    [RoutePrefix("api/productlist")]
    public class ProductListApiController : BaseApiController
    {
        private IProductListingService _productListingService;

        public ProductListApiController(IProductListingService productListingService)
        {
            _productListingService = productListingService;
        }
        public List<ProductTileViewModel> GetProductList(string brand, string category, decimal price,bool sortAlphabet=false)
        {
            var result = _productListingService.GetListProduct(brand, price, category);
            if (result != null)
            {
                if (sortAlphabet) result = result.OrderBy(x => x.DisplayName);
                return result.ToList();
            }
            return new List<ProductTileViewModel>();
        }
    }
}
