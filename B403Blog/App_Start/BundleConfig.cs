using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace B403Blog.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Content/AdminContent/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/Content/AdminContent/vendor/jquery-easing/jquery.easing.min.js",
                "~/Content/AdminContent/js/sb-admin-2.min.js",
                "~/vendor/chart.js/Chart.min.js",
                "~/Content/AdminContent/js/demo/chart-area-demo.js",
                "~/Content/AdminContent/js/demo/chart-pie-demo.js",
                "~/Content/Datatables/jquery.dataTables.min.js",
                "~/Content/Datatables/dataTables.bootstrap.min.js"
                ));
        }
    }
}