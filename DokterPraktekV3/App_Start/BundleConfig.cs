using System.Web;
using System.Web.Optimization;

namespace DokterPraktekV3
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // New Bundle
            bundles.Add(new StyleBundle("~/Content/assets/css").Include(
                      "~/Content/assets/css/bootstrap.min.css",
                      "~/Content/assets/css/font-awesome.min.css",
                      "~/Content/assets/css/themify-icons.css",
                      "~/Content/assets/css/metisMenu.css",
                      "~/Content/assets/css/owl.carousel.min.css",
                      "~/Content/assets/css/slicknav.min.css",
                      "~/Content/assets/css/typography.css",
                      "~/Content/assets/css/default-css.css",
                      "~/Content/assets/css/styles.css",
                      "~/Content/assets/css/responsive.css",
                      "~/Content/assets/sweetalert/dist/sweetalert.css"));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/assets").Include(
                      "~/Scripts/assets/js/vendor/modernizr-2.8.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/assets/js").Include(
                      "~/Scripts/assets/js/vendor/jquery-2.2.4.min.js",
                      "~/Scripts/assets/jquery-ui/jquery-ui.js",
                      "~/Scripts/assets/js/popper.min.js",
                      "~/Scripts/assets/js/bootstrap.min.js",
                      "~/Scripts/assets/js/owl.carousel.min.js",
                      "~/Scripts/assets/js/metisMenu.min.js",
                      "~/Scripts/assets/js/jquery.slimscroll.min.js",
                      "~/Scripts/assets/js/jquery.slicknav.min.js",
                      "~/Scripts/sweetalert.js",
                      "~/Content/assets/sweetalert/dist/sweetalert.min.js"));

        }
    }
}
