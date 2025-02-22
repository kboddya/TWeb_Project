using System.Web;
using System.Web.Optimization;

namespace BookShopProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/normalize/css").Include("~/Content/Styles/normalize.css",
                new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/style/css").Include("~/Content/Styles/style.css",
                new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/vendor/css").Include("~/Content/Styles/vendor.css",
                new CssRewriteUrlTransform()));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Scrips/jquery-1.11.0.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr/js").Include("~/Scrips/modernizr.js"));
            bundles.Add(new ScriptBundle("~/bundles/plugins/js").Include("~/Scrips/plugins.js"));
            bundles.Add(new ScriptBundle("~/bundles/script/js").Include("~/Scrips/script.js"));
            bundles.Add(new ScriptBundle("~/bundles/slideNav/js").Include("~/Scrips/slideNav.js"));
            bundles.Add(new ScriptBundle("~/bundles/slideNav/min/js").Include("~/Scrips/slideNav.min.js"));
        }
    }
}