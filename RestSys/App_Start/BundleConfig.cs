using System.Web;
using System.Web.Optimization;

namespace RestSys
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/site.css"));
        }
    }
}
