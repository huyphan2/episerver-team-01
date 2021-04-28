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
    [ContentType(DisplayName = "News", GUID = "6A1AA0C3-4598-440F-B604-DC13F4CB1B3A")]
    public class NewsBlock : BlockData
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
        public virtual ContentArea ContentArea { get; set; }

    }
}