using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPiServer.Reference.Commerce.Site.Features.News.Blocks
{
    [ContentType(DisplayName = "Blog News", GUID = "57BED5DB-544B-4C76-8D54-7BE42A7A7869")]
    public class BlogNewsBlock : BlockData
    {
        [CultureSpecific]
        [Display(
              GroupName = SystemTabNames.Content,
              Order = 1)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(
             Name = "Content",
             GroupName = SystemTabNames.Content,
             Order = 10)]
        public virtual XhtmlString Description { get; set; }

        [CultureSpecific]
        [Display(
             Name = "Content Area",
             GroupName = SystemTabNames.Content,
             Order = 20)]
        [AllowedTypes(typeof(BlogNewsDetailBlock))]
        public virtual ContentArea ContentArea { get; set; }

    }
}