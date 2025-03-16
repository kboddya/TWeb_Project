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
                "~/Content/Styles/icomoon.css", "~/Content/slick.css", "~/Content/slick-theme.css", "~/Content/about.css", "~/Content/offer.css", "~/Content/review.css"));


<<<<<<< HEAD
            bundles.Add(new ScriptBundle("~/bundles/scrips").Include("~/Scrips/bootstrap.bundle.min.js",
                "~/Scrips/jquery-1.11.0.min.js", "~/Scrips/modernizr.js", "~/Scrips/plugins.js", "~/Scrips/script.js",
                "~/Scrips/slideNav.js", "~/Scrips/slideNav.min.js", "~/Scrips/about.js", "~/Scrips/slick.min.js"));

=======
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/jquery-1.11.0.min.js", "~/Scripts/modernizr.js", "~/Scripts/plugins.js", "~/Scripts/script.js",
                "~/Scripts/slideNav.js", "~/Scripts/slideNav.min.js", "~/Scripts/favicon.js", "~/Scripts/favicon.min.js"));
>>>>>>> 883fbf349b9e918f813100faa9a5a3a1285159d4
        }
    }
}