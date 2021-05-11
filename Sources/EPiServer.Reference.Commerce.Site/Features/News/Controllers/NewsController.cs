using EPiServer.Reference.Commerce.Site.Features.News.Blocks;
using EPiServer.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.News.Controllers
{
    public class NewsController : BlockController<NewsBlock>
    {
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public override ActionResult Index(NewsBlock currentBlock)
        {
            return PartialView(currentBlock);
        }
    }
}