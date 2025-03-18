using System.Web;
using System.Web.Optimization;

namespace BookShopProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").Include("~/Content/Styles/custom.css",
                "~/Content/Styles/bootstrap.min.css",
                "~/Content/Styles/normalize.css", "~/Content/Styles/style.css", "~/Content/Styles/vendor.css",
                "~/Content/Styles/icomoon.css", "~/Content/slick.css", "~/Content/slick-theme.css",
                "~/Content/about.css", "~/Content/offer.css", "~/Content/review.css"));


            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/jquery-1.11.0.min.js", "~/Scripts/modernizr.js", "~/Scripts/plugins.js",
                "~/Scripts/script.js",
                "~/Scripts/slideNav.js", "~/Scripts/slideNav.min.js", "~/Scripts/favicon.js",
                "~/Scripts/favicon.min.js", "~/Scripts/about.js", "~/Scripts/slick.min.js"));
        }
    }
}