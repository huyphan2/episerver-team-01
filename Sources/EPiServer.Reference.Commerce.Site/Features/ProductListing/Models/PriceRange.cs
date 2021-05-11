namespace EPiServer.Reference.Commerce.Site.Features.ProductListing.Models
{
    public class PriceRange
    {
        public virtual decimal From { get; set; }
        public virtual decimal To { get; set; }
        public override string ToString()
        {
            return $"{From.ToString("C0")}-{To.ToString("C0")}";
        }
    }
}