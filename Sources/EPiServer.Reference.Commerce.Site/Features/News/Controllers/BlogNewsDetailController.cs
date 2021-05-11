using EPiServer.Reference.Commerce.Site.Features.News.Blocks;
using EPiServer.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.News.Controllers
{
    public class BlogNewsDetailController : BlockController<BlogNewsDetailBlock>
    {
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public override ActionResult Index(BlogNewsDetailBlock currentBlock)
        {
            var viewName = $"~/Views/News/{currentBlock.GetOriginalType().Name.Replace("Block", string.Empty)}.cshtml";
            return PartialView(viewName, currentBlock);
        }
    }
}