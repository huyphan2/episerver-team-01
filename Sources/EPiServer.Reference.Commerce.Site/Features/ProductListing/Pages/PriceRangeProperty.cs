using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Reference.Commerce.Site.Features.ProductListing.Models;

namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Pages
{
    [PropertyDefinitionTypePlugIn]
    public class PriceRangeProperty : PropertyList<PriceRange>
    {
    }
}