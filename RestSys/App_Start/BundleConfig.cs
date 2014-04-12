using System.Web;
using System.Web.Optimization;

namespace RestSys
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-ui-sortable.js",
                        "~/Scripts/controllers.js"
                        ));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/site.css",
                      "~/Content/angelfish.css"));
        }
    }
}
