using System.ComponentModel.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Validation
{
    public class LinkItemCollectionMaxItemsCount : ValidationAttribute
    {
        public int Limit { get; }

        public LinkItemCollectionMaxItemsCount(int limit)
        {
            Limit = limit;
        }

        public override bool IsValid(object value)
        {
            return ValidateProperty(value as LinkItemCollection);
        }

        private bool ValidateProperty(LinkItemCollection item)
        {
            return item == null || item.Count <= Limit;
        }
    }
}