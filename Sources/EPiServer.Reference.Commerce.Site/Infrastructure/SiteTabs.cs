using EPiServer.DataAnnotations;
using EPiServer.Security;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    [GroupDefinitions]
    public static class SiteTabs
    {
        [Display(Order = 100)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string SiteStructure = "Site structure";

        [Display(Order = 110)]
        [RequiredAccess(AccessLevel.Edit)]
        public const string MailTemplates = "Mail templates";
        #region Products Listing

        public const string CarouselBanner = "Carousel Banner";
        public const string NavigationContent = "Navigation Content";
        public const string Products = "Products";
        #endregion
        
    }

    public static class UIHints
    {
        public const string Images = "Images";
    }
}