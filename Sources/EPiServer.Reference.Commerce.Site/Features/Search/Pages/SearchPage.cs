using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;

namespace EPiServer.Reference.Commerce.Site.Features.Search.Pages
{
    [ContentType(DisplayName = "Search page", GUID = "6e0c84de-bd17-43ee-9019-04f08c7fcf8d", Description = "", AvailableInEditMode = false)]
    public class SearchPage : PageData
    {
        [Ignore]
        public List<ProductTileViewModel> Products{ get; set; }
    }
}