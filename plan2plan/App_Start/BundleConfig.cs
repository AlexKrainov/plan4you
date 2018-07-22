using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace plan2plan.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Style
            // Bootstrap Styles
            bundles.Add(new StyleBundle("~/Content/bootstrapCss").Include(
                "~/Content/bootstrap.min.css", new CssRewriteUrlTransform()
            ));

            // Bootstrap icon Styles  https://useiconic.com/
            bundles.Add(new StyleBundle("~/Content/bootstrapIcon").Include(
                "~/Content/bootstrap_icons/font/css/open-iconic-foundation.min.css"
            //, "~/Content/bootstrap_icons/font/css/open-iconic-bootstrap.min.css"
            //, "~/Content/bootstrap_icons/font/css/open-iconic.min.css"
            ));

            //bootstrap float-label (https://github.com/tonystar/float-label-css)
            //bundles.Add(new StyleBundle("~/Content/bootstrap-float-label").Include(
            //    "~/Content/bootstrap-float-label/bootstrap-float-label.css"
            //));

            // TemplateSite Libraries CSS Files
            bundles.Add(new StyleBundle("~/TemplateSite/lib/css").Include(
               "~/TemplateSite/lib/animate/animate.css",
               "~/TemplateSite/lib/ionicons/css/ionicons.css",
               "~/TemplateSite/lib/owlcarousel/assets/owl.carousel.css",
               "~/TemplateSite/lib/magnific-popup/magnific-popup.css",
               "~/TemplateSite/lib/ionicons/css/ionicons.css"
           ));

            // Mine Stylesheet File
            bundles.Add(new StyleBundle("~/TemplateSite/styleCSS").Include(
                "~/TemplateSite/css/style.css", new CssRewriteUrlTransform()
            ));

            // Mine styles
            bundles.Add(new StyleBundle("~/Content/siteCss").Include(
                "~/Content/Site.css", new CssRewriteUrlTransform()
            ));

            //Bootstrap-table
            bundles.Add(new StyleBundle("~/Vendor/Bootstrap-table-css").Include(
               "~/Scripts/bootstrap/bootstrap_table/bootstrap-table.min.css",
               "~/Scripts/bootstrap/bootstrap_table/extensions/sticky-header/bootstrap-table-sticky-header.css"
           ));

            //DataTables ~/Style/Vendor/DataTables
            bundles.Add(new StyleBundle("~/Style/Vendor/DataTables").Include(
             //"~/Content/DataTables/media/css/jquery.dataTables.min.css",
             "~/Content/DataTables/media/css/dataTables.bootstrap4.min.css"
         ));
            #endregion

            #region Script
            //Vendor
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //                "~/TemplateSite/lib/jquery/jquery.min.js"
            //    ));

            //Template site script
            bundles.Add(new ScriptBundle("~/bundles/TemplateSite/Vendor/Script").Include(
          //"~/TemplateSite/lib/jquery/jquery-migrate.min.js",
          "~/TemplateSite/lib/bootstrap/js/bootstrap.bundle.min.js",
          "~/TemplateSite/lib/easing/easing.js",
          "~/TemplateSite/lib/superfish/hoverIntent.js",
          "~/TemplateSite/lib/superfish/superfish.js",
          "~/TemplateSite/lib/wow/wow.js",
          "~/TemplateSite/lib/owlcarousel/owl.carousel.js",
          "~/TemplateSite/lib/magnific-popup/magnific-popup.js",
          "~/TemplateSite/lib/sticky/sticky.js",
          "~/TemplateSite/js/main.js" // <!-- Template Main Javascript File -->
        ));

            //Template site script
            bundles.Add(new ScriptBundle("~/Vendor/Bootstrap-table-script").Include(
          "~/Scripts/bootstrap/bootstrap_table/bootstrap-table.min.js",
          "~/Scripts/bootstrap/bootstrap_table/extensions/sticky-header/bootstrap-table-sticky-header.js",
          "~/Scripts/bootstrap/bootstrap_table/locale/bootstrap-table-ru-RU.js",
          "~/Scripts/bootstrap/bootstrap_table/MyScript/site_table_bootstrapjs.js"
        ));



            //Mine script
            bundles.Add(new ScriptBundle("~/bundles/SiteScript").Include(
         "~/Scripts/statistics.js",
         "~/Scripts/action.js",
         "~/Scripts/site.js",
         "~/Scripts/user/auth.js"
        ));

            //Vue js
            bundles.Add(new ScriptBundle("~/bundles/js/vue").Include(
        "~/Scripts/vue.min.js",
        "~/Scripts/vue_components/like_component.js",
        "~/Scripts/vue_components/download_componentjs.js"
       ));

            //Ajax jquery_unobtrusive 
            bundles.Add(new ScriptBundle("~/bundles/jquery_unobtrusive_ajax").Include(
                            "~/Scripts/jquery/jquery.unobtrusive-ajax.min.js"
                ));


            //Mine file_worker
            bundles.Add(new ScriptBundle("~/Script/file_worker").Include(
                            "~/Scripts/file_worker.js"
                ));

            //Mine file_worker
            bundles.Add(new ScriptBundle("~/Script/contactform").Include(
                            "~/TemplateSite/contactform/contactform.js"
                ));

            //dataTables
            bundles.Add(new ScriptBundle("~/Script/Vendor/DataTable").Include(
      "~/Scripts/DataTables/media/js/jquery.dataTables.min.js",
          "~/Scripts/DataTables/media/js/dataTables.bootstrap4.min.js"
     ));
            #endregion

            //  BundleTable.EnableOptimizations = true;





        }
    }
}