using System.Web;
using System.Web.Optimization;

namespace BookShopProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").Include("~/Content/Styles/bootstrap.min.css", "~/Content/Styles/normalize.css", "~/Content/Styles/style.css", "~/Content/Styles/vendor.css"));

            
            bundles.Add(new ScriptBundle("~/bundles/scrips").Include("~/Scrips/bootstrap.bundle.min.js",
                "~/Scrips/jquery-1.11.0.min.js", "~/Scrips/modernizr.js", "~/Scrips/plugins.js", "~/Scrips/script.js","~/Scrips/slideNav.js","~/Scrips/slideNav.min.js"));
        }
    }
}