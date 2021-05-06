using System.Web.Optimization;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap*"));
            bundles.Add(new ScriptBundle("~/bundles/mustache").Include("~/Scripts/mustache.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/js/*.js"));
            bundles.Add(new StyleBundle("~/styles/bundled").Include("~/Styles/style.css"));

            bundles.Add(new StyleBundle("~/bundles/reset").Include("~/Static/css/reset.css"));
            //css
            bundles.Add(new StyleBundle("~/bundles/style")
                .Include("~/Static/libs/bootstrap.min.css")
                .Include("~/Static/libs/font-awesome.css")
                .Include("~/Static/libs/slick.min.css")
                .Include("~/Static/css/font.css") 
                .Include("~/Static/css/header.css")
                .Include("~/Static/css/product-card.css")
                .Include("~/Static/css/news-static.css")
                .Include("~/Static/css/shop-category.css")
                .Include("~/Static/css/product-list-component.css")
                .Include("~/Static/css/footer.css")
                .Include("~/Static/css/styles.css")
                .Include("~/Static/css/product-list-page.css")
                .Include("~/Static/css/home.css")
            );
 

            //js
            //bundles.Add(new StyleBundle("~/bundles/jquery").Include("~/Static/libs/jquery-3.6.0.min.js"));
            bundles.Add(new StyleBundle("~/bundles/javascript")
                .Include("~/Static/libs/slick.min.js")
                .Include("~/Static/script/index.js")); 

        }
    }
} 