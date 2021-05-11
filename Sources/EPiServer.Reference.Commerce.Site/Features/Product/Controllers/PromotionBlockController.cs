using EPiServer;
using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Features.Market.Services;
using EPiServer.Reference.Commerce.Site.Features.Product.Blocks;
using EPiServer.Reference.Commerce.Site.Features.Product.Services;
using EPiServer.Reference.Commerce.Site.Features.Start.ViewModels;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.Product.Controllers
{
    public class PromotionBlockController : BlockController<PromotionBlock>
    {
        private readonly IContentLoader _contentLoader;
        private readonly ICurrentMarket _currentMarket;
        private readonly MarketContentLoader _marketContentFilter;
        private readonly IProductService _productService;
        public PromotionBlockController(IContentLoader contentLoader,
           ICurrentMarket currentMarket,
            IProductService productService,
            MarketContentLoader marketContentFilter)
        {
            _contentLoader = contentLoader;
            _currentMarket = currentMarket;
            _marketContentFilter = marketContentFilter;
            _productService = productService;
        }
        public override ActionResult Index(PromotionBlock currentBlock)
        {
            var promotions = GetActivePromotions();
            return PartialView(promotions);
        }
        private IEnumerable<PromotionViewModel> GetActivePromotions()
        {
            var promotions = new List<PromotionViewModel>();

            var promotionItemGroups = _marketContentFilter.GetPromotionItemsForMarket(_currentMarket.GetCurrentMarket()).GroupBy(x => x.Promotion);

            foreach (var promotionGroup in promotionItemGroups)
            {
                var promotionItems = promotionGroup.First();
                promotions.Add(new PromotionViewModel()
                {
                    Name = promotionGroup.Key.Name,
                    BannerImage = promotionGroup.Key.Banner,
                    SelectionType = promotionItems.Condition.Type,
                   
                });
            }

            return promotions;
        }
    }
}
