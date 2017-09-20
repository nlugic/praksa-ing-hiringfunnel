using System.Web.Optimization;

namespace HiringFunnel.Web
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/flatpickr").Include(
                        "~/Scripts/flatpickr.min.js",
                        "~/Scripts/flatpickr_sr.js"));

            bundles.Add(new ScriptBundle("~/bundles/selectric").Include(
                        "~/Scripts/jquery.selectric.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/usercontact").Include(
                        "~/Scripts/hfScripts/data-forms-user-contact.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/hfStyle.css"));

            bundles.Add(new StyleBundle("~/Content/datatables/css").Include(
                        "~/Content/Datatables/dataTables.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/flatpickr").Include(
                        "~/Content/flatpickr_airbnb.css"));

            bundles.Add(new StyleBundle("~/Content/selectric").Include(
                        "~/Content/selectric.css"));
        }

    }
}
