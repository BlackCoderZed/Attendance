using System.Web;
using System.Web.Optimization;

namespace HRAttendance
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/commonLibs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootbox.js",
                //"~/Scripts/modernizr-*",
                "~/Scripts/popper.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/dataTables.jqueryui.js",
                "~/Scripts/DataTables/dataTables.rowReorder.js",
                "~/Scripts/DataTables/dataTables.responsive.js",
                "~/Scripts/Site/common.js"
            ));

            bundles.Add(new Bundle("~/bundles/operationLibs").Include(
                "~/Scripts/Site/emolpyee-list-view.js",
                "~/Scripts/Site/employee-reg-dlg.js",
                "~/Scripts/Site/attendance-view.js"

            ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.theme.css",
                      "~/Content/jquery.mCustomScrollbar.min.css",
                      "~/Content/DataTables/css/rowReorder.dataTables.css",
                      "~/Content/DataTables/css/responsive.dataTables.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/dataTables.jqueryui.css",
                      "~/Content/DataTables/css/buttons.dataTables.css",
                      "~/Content/DataTables/css/colReorder.dataTables.css",
                      "~/Content/site.css"));
        }
    }
}
