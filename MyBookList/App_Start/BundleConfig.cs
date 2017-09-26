using System.Web;
using System.Web.Optimization;

namespace MyBookList
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/scripts/datatables/jquery.datatables.js",
                        "~/scripts/datatables/datatables.bootstrap.js",
                        "~/scripts/toastr.js"
                        ));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/jquery.unobtrusive-ajax.min.js",
            "~/Scripts/jquery.validate*",
            "~/Scripts/jquery.validate.unobtrusive.js",
            "~/Scripts/jquery.validate.unobtrusive.min.js"));
            // Custom.     
            bundles.Add(new ScriptBundle("~/bundles/custom-modal").Include(
                       "~/Scripts/custom-modal.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap-slate.css",
                        "~/Content/DataTables/css/dataTables.bootstrap.css",
                        "~/Content/toastr.css",
                        "~/Content/site.css"
                        ));

        }
    }
}