using System.Collections.Generic;
using System.EnterpriseServices;
using System.Threading;
using EPiServer.Reference.Commerce.Site.Features.Search.Pages;
using EPiServer.Reference.Commerce.Site.Features.Search.Services;
using EPiServer.Reference.Commerce.Site.Features.Search.ViewModelFactories;
using EPiServer.Reference.Commerce.Site.Features.Search.ViewModels;
using EPiServer.Web.Mvc;
using System.Web.Mvc;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Services;

namespace EPiServer.Reference.Commerce.Site.Features.Search.Controllers
{
    public class SearchController : PageController<SearchPage>
    {
        private readonly SearchViewModelFactory _viewModelFactory;
        private readonly ISearchService _searchService;
        private readonly IProductListingService _productListingService;
        private string currentLanguage;
        public SearchController(
            SearchViewModelFactory viewModelFactory, 
            ISearchService searchService, IProductListingService productListingService)
        {
            _viewModelFactory = viewModelFactory;
            _searchService = searchService;
            _productListingService = productListingService;
            currentLanguage= Thread.CurrentThread.CurrentUICulture.Name;
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(SearchPage currentPage, string query)
        {
            if (currentPage == null) currentPage = new SearchPage();
            currentPage.Products = _productListingService.SearchWildcardProduct(query, currentLanguage);

            return View("Index",currentPage.Products);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QuickSearch(string query = "")
        {
            //var result = _searchService.QuickSearch(query);
            var result = _productListingService.SearchWildcardProduct(query, currentLanguage);
            return View("_QuickSearch", result);
        }
    }
}