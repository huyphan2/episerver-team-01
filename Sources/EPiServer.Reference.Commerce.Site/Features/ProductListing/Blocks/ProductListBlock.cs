using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Reference.Commerce.Site.Features.Product.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Models;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Blocks
{
    [ContentType(DisplayName = "ProductListBlock", GUID = "63a075f8-8628-4646-839b-2c53dcc03df4", Description = "")]
    public class ProductListBlock : BlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Filter Title",
            Description = "Shop by text",
            GroupName = SiteTabs.Products,
            Order = 1)]
        public virtual string FilterTitle { get; set; }
        [CultureSpecific]
        [Display(
            Name = "ProductListText",
            Description = "All Product Text",
            GroupName = SiteTabs.Products,
            Order = 1)]
        public virtual string ProductListText { get; set; }
        [CultureSpecific]
        [Display(
            Name = "View As Text",
            GroupName = SiteTabs.Products,
            Order = 1)]
        public virtual string ViewAsText { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Sort Text",
            GroupName = SiteTabs.Products,
            Order = 1)]
        public virtual string SortText { get; set; }
        //
        [CultureSpecific]
        [Display(
            Name = "Price Filter",
            Description = "List price range for searching",
            GroupName = SiteTabs.Products,
            Order = 2)]
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<PriceRange>))]
        public virtual IList<PriceRange> PriceFilter { get; set; }
        [Display(
            Name = "Categories",
            GroupName = SiteTabs.Products,
            Order = 2)]
        [AllowedTypes(typeof(FashionNode))]
        public virtual ContentArea CategoryCollection { get; set; }
        [Display(
            Name = "Brands",
            GroupName = SiteTabs.Products,
            Order = 2)]
        public virtual ContentArea BrandCollection { get; set; }
        [Display(
            Name = "Categories",
            GroupName = SiteTabs.Products,
            Order = 2)]
        [ScaffoldColumn(false)]
        public virtual LinkItemCollection Categories{ get; set; }
        [Display(
            Name = "Brands",
            GroupName = SiteTabs.Products,
            Order = 2)]
        [ScaffoldColumn(false)]
        public virtual LinkItemCollection Brands{ get; set; }
        [Ignore]
        public FilterParams FilterParams { get; set; }
        [Ignore]
        public List<ProductTileViewModel> Products { get; set; }
        [Ignore]
        public FilterParam SelectedParams { get; set; }


    }
}