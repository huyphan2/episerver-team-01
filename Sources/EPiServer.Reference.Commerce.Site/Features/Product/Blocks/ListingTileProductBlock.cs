using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.Product.Models;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPiServer.Reference.Commerce.Site.Features.Product.Blocks
{
    [ContentType(DisplayName = "ListingTileProductBlock", GUID = "2fe5fbc4-d715-49ab-80fe-b98b72bafea2", Description = "")]
    public class ListingTileProductBlock : BlockData
    {
        [Display(Name = "Title", Description = "Title of list product")]
        public virtual string Title { get; set; }
        [Display(Name = "CatalogContent", Description = "Get newest product of catalog to display")]
        [AllowedTypes(AllowedTypes = new[] { typeof(NodeContent), typeof(CatalogContent) })]
        public virtual ContentReference ReferContent { get; set; }

        [Display(Name = "NumberOfItem", Description = "Get list product with max number of item")]
        public virtual int NumberOfItem { get; set; }

        [Display(Name = "OrderByAttribute", Description = "Order field")]
        [SelectOne(SelectionFactoryType = typeof(OrderProductSelectionFactory))]
        public virtual string OrderByAttribute { get; set; }
    }
 
    public class OrderProductSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[] {
                new SelectItem() { Text = "StartDatePublish", Value = nameof(FashionProduct.StartPublish) }
                , new SelectItem() { Text = "Ranking", Value =  nameof(FashionProduct.Ranking) }
            };
        }
    }
}