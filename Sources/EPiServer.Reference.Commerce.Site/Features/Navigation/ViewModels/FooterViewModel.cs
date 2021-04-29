using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Features.Navigation.Models;
using EPiServer.SpecializedProperties;

namespace EPiServer.Reference.Commerce.Site.Features.Navigation.ViewModels
{
    public class FooterViewModel
    {
        public LinkItemCollection FooterLinks { get; set; }
        public XhtmlString FooterBlock { get; set; }
    }
}