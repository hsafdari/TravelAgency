using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ParvazPardaz.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            IItemTransform cssFixer = new CssRewriteUrlTransform();


            //Layout cLient css
            bundles.Add(new StyleBundle("~/bundles/TourClientCss")
            .Include("~/ClientTheme/Charteran/Tour/TourCustomStyles.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/reset.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/font.css", cssFixer)
            //.Include("~/ClientTheme/Blog/Tour/css/persian-datepicker.min.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/xl.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/l.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/m.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/s.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/TourMobile.css", cssFixer)
            .Include("~/Content/assets/plugins/Select2_V4/css/select2.css", cssFixer)
            .Include("~/Content/toastr/toastr.min.css", cssFixer)
            .Include("~/ClientTheme/Blog/Tour/css/LayoutCustomStyle.css", cssFixer)
            );

            bundles.Add(new StyleBundle("~/bundles/HomeCustomCss")
                .Include("~/ClientTheme/Blog/Tour/css/HomeCustomStyle.css", cssFixer)
            );

            bundles.Add(new StyleBundle("~/bundles/ConfirmCustomCss")
                .Include("~/ClientTheme/Blog/Tour/css/ConfirmCustomStyle.css", cssFixer)
            );

            bundles.Add(new StyleBundle("~/bundles/TourCustomCss")
                //.Include("~/ClientTheme/Blog/Tour/css/persian-datepicker.min.css", cssFixer)
                .Include("~/ClientTheme/Blog/Tour/css/TourCustomStyle.css", cssFixer)
            );

            ////Magazine client css
            //bundles.Add(new StyleBundle("~/bundles/MagClientCss")
            //.Include("~/ClientTheme/Charteran/Magazine/MagCustomStyles.css", cssFixer)
            //.Include("~/ClientTheme/Blog/Magazine/css/reset.css", cssFixer)
            //.Include("~/ClientTheme/Blog/Magazine/css/font.css", cssFixer)
            //.Include("~/ClientTheme/Blog/Magazine/css/xl.css", cssFixer)
            //.Include("~/ClientTheme/Blog/Magazine/css/l.css", cssFixer)
            //.Include("~/ClientTheme/Blog/Magazine/css/m.css", cssFixer)
            //.Include("~/ClientTheme/Blog/Magazine/css/s.css", cssFixer)
            //.Include("~/Content/toastr/toastr.min.css", cssFixer)
            //);

            ////Magazine client js
            //bundles.Add(new ScriptBundle("~/bundles/MagClientJs").Include(
            //           "~/ClientTheme/Blog/Magazine/js/jq.js",
            //           "~/ClientTheme/Blog/Magazine/js/custom.js"
            //           ));

            bundles.Add(new StyleBundle("~/bundles/blogcss")
           //.Include("~/SiteClientTheme/css/BlogMenuStyle.css", cssFixer)
           .Include("~/ClientTheme/libraries/bootstrap/RTL/css/bootstrap-rtl.min.css", cssFixer)
           .Include("~/ClientTheme/libraries/owl-carousel/owl.carousel.css", cssFixer)
           .Include("~/ClientTheme/libraries/owl-carousel/owl.theme.css", cssFixer)
           .Include("~/ClientTheme/libraries/flexslider/flexslider.css", cssFixer)
           .Include("~/ClientTheme/libraries/fonts/font-awesome.min.css", cssFixer)
           .Include("~/ClientTheme/libraries/animate/animate.min.css", cssFixer)
           .Include("~/ClientTheme/css/components.css", cssFixer)
           .Include("~/ClientTheme/css/style.css", cssFixer)
           .Include("~/ClientTheme/css/media.css", cssFixer)
           .Include("~/ClientTheme/css/RTL.css", cssFixer)
           .Include("~/ClientTheme/css/Blue.css", cssFixer)
           .Include("~/ClientTheme/css/CustomStyle.css", cssFixer)
           .Include("~/ClientTheme/css/fontiran.css", cssFixer)
           .Include("~/ClientTheme/css/IranianSans.css", cssFixer)
           //.Include("~/SiteClientTheme/css/megaMenu.css", cssFixer)
           //.Include("~/ClientTheme/css/menuColor.css", cssFixer)
           .Include("~/SiteClientTheme/css/font-awesome.css", cssFixer)
           .Include("~/SiteClientTheme/fonts/fi/flaticon.css", cssFixer)
           /*بررسی شود*/
           .Include("~/ClientTheme/css/Menu.css", cssFixer)
           .Include("~/ClientTheme/css/simple-sidebar.css", cssFixer)
           .Include("~/Content/toastr/toastr.min.css", cssFixer)          
           .Include("~/SiteClientTheme/custom/BlogCustomStyles.css", cssFixer)
           .Include("~/SiteClientTheme/custom/custom-footer.css", cssFixer)
           .Include("~/ClientTheme/css/Footer.css", cssFixer)
           );
            /*بررسی شود*/
            bundles.Add(new ScriptBundle("~/BlogLayoutTopJs").Include(
                "~/ClientTheme/libraries/jquery.min.js",
                "~/Scripts/toastr/toastr.min.js"               
                ));
            bundles.Add(new ScriptBundle("~/bundles/blogcjs").Include(
             //"~/ClientTheme/libraries/jquery.min.js",
             "~/ClientTheme/libraries/jquery.easing.min.js",
             "~/ClientTheme/libraries/bootstrap/RTL/js/bootstrap-rtl.min.js",
             "~/ClientTheme/libraries/jquery.animateNumber.min.js",
             "~/ClientTheme/libraries/jquery.appear.js",
             "~/ClientTheme/libraries/jquery.knob.js",

             "~/ClientTheme/libraries/wow.min.js",
             "~/ClientTheme/libraries/flexslider/jquery.flexslider.js", /*-min*/
             "~/ClientTheme/libraries/owl-carousel/owl.carousel.min.js",
             "~/SiteClientTheme/js/owl.carousel.js", /*قبلا نبود افزودیم*/
             "~/ClientTheme/libraries/expanding-search/modernizr.custom.js",

             "~/ClientTheme/libraries/expanding-search/classie.js",
             "~/ClientTheme/libraries/jssor.js",
             "~/ClientTheme/libraries/jssor.slider.js",
             //"~/ClientTheme/libraries/jquery.marquee.js",
             "~/ClientTheme/libraries/Marquee/jquery.marquee.min.js",
             "~/ClientTheme/js/functions.js"
         //بررسی شود
         //"~/Plugins/cool-share/plugin.js",
         //"~/Plugins/cool-share/shareconfig.js"
         ));

            bundles.Add(new ScriptBundle("~/bundles/TourClientJs").Include(
                        "~/Scripts/nets/jquery-2.0.3.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/ClientTheme/Blog/Tour/js/jq.js",
                        "~/ClientTheme/Blog/Tour/js/custom.js",
                        "~/ClientTheme/Blog/Tour/js/TourCustomScripts.js",
                        //"~/ClientTheme/Blog/Tour/js/persian-date.min.js",
                        //"~/ClientTheme/Blog/Tour/js/persian-datepicker.min.js",
                        "~/Content/assets/plugins/Select2_V4/js/select2.js",
                        "~/Scripts/toastr/toastr.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/nets/jquery-2.0.3.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryClient").Include(
                "~/SiteClientTheme/js/jquery.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                   "~/Scripts/jquery.validate*",
                   "~/Scripts/jquery.validate.unobtrusive*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
                       ));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                            "~/Content/themes/base/jquery-ui.css"
                         ));

            bundles.Add(new StyleBundle("~/bundles/assets/css").Include(
                "~/Content/assets/plugins/bootstrap/css/bootstrap.min.css",
                //"~/Content/assets/plugins/font-awesome/css/font-awesome.min.css",
                //"~/Content/assets/fonts/style.css",
                "~/Content/assets/css/main.css",
                "~/Content/assets/css/main-responsive.css",
                "~/Content/assets/plugins/iCheck/skins/all.css",
                "~/Content/assets/plugins/bootstrap-colorpalette/css/bootstrap-colorpalette.css",
                "~/Content/assets/plugins/perfect-scrollbar/src/perfect-scrollbar-rtl.css",
                "~/Content/assets/Admin.css",
                "~/Content/assets/css/style.css",
                "~/Content/assets/plugins/fullcalendar/fullcalendar/fullcalendar.css",
                "~/Content/assets/plugins/bootstrap-fileupload/bootstrap-fileupload.min.css",
                //"~/Content/assets/plugins/select2/select2.css",
                "~/Content/assets/plugins/Select2_V4/css/select2.css",
                "~/Content/assets/css/theme_navy.css",
                "~/SiteClientTheme/custom/custom-footer.css"
                )
                .Include("~/Content/assets/plugins/font-awesome/css/font-awesome.min.css", cssFixer)
                 .Include("~/Content/assets/fonts/style.css", cssFixer)

                );

            bundles.Add(new ScriptBundle("~/bundles/assets/js").Include(
                "~/Scripts/jquery-ui-1.12.0.js",
                "~/Content/assets/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/assets/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Content/assets/plugins/blockUI/jquery.blockUI.js",
                "~/Content/assets/plugins/iCheck/jquery.icheck.min.js",
                "~/Content/assets/plugins/perfect-scrollbar/src/jquery.mousewheel.js",
                "~/Content/assets/plugins/perfect-scrollbar/src/perfect-scrollbar-rtl.js",
                "~/Content/assets/plugins/less/less-1.5.0.min.js",
                "~/Content/assets/plugins/jquery-cookie/jquery.cookie.js",
                "~/Content/assets/plugins/bootstrap-colorpalette/js/bootstrap-colorpalette.js",
                //"~/Content/assets/plugins/select2/select2.js", 
                "~/Content/assets/plugins/Select2_V4/js/select2.js",
                "~/Content/assets/js/main.js"
                ));

            bundles.Add(new StyleBundle("~/Content/toastr/css").Include("~/Content/toastr/toastr.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/toastr/js").Include("~/Scripts/toastr/toastr.min.js"));

            bundles.Add(new StyleBundle("~/Plugins/css")
                .Include("~/Plugins/jQuery.filer-master/css/jquery.filer.css", cssFixer)
                .Include("~/Content/admin/custom.css", cssFixer)
                .Include("~/Plugins/jQuery.filer-master/css/themes/jquery.filer-dragdropbox-theme.css", cssFixer)
                .Include(
                //"~/Plugins/jQuery.filer-master/css/jquery.filer.css",
                //"~/Content/admin/custom.css",
                //"~/Plugins/jQuery.filer-master/css/themes/jquery.filer-dragdropbox-theme.css",
                "~/Plugins/bootstrap-touchspin-master/bootstrap-touchspin-master/src/jquery.bootstrap-touchspin.css",
                "~/Content/assets/plugins/bootstrap-fileupload/bootstrap-fileupload.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Plugins/js").Include(
                "~/Plugins/bootstrap-touchspin-master/bootstrap-touchspin-master/src/jquery.bootstrap-touchspin.js",
                "~/Plugins/jQuery.filer-master/js/jquery.filer.js",
                "~/Content/assets/plugins/bootstrap-fileupload/bootstrap-fileupload.min.js",
                "~/Plugins/jqEasyCharCounter/jquery.jqEasyCharCounter.js",
                "~/Plugins/momentDate/moment.min.js",
                "~/Plugins/momentDate/moment-jalaali.js"
            ));

            #region TinyMCE Js
            bundles.Add(new ScriptBundle("~/PluginsBndl/enUS/js").Include(
                    "~/Plugins/tinymce/tinymce.min.js"
                ));
            bundles.Add(new ScriptBundle("~/PluginsBndl/faIR/js").Include(
                "~/Plugins/tinymce/tinymce.min.js",
                "~/Plugins/tinymce/langs/fa.js"
            ));
            bundles.Add(new ScriptBundle("~/PluginsBndl/arIQ/js").Include(
                "~/Plugins/tinymce/tinymce.min.js",
                "~/Plugins/tinymce/langs/ar.js"
            ));
            #endregion

            #region Kendo GridView Css
            bundles.Add(new StyleBundle("~/content/kendo/LTR/css").Include(
                    "~/content/kendo/2014.3.1314/kendo.common.min.css",
                    "~/content/kendo/2014.3.1314/kendo.default.min.css",
                    "~/Content/kendo/KendoGridViewLTR.css"
                ));
            bundles.Add(new StyleBundle("~/content/kendo/RTL/css").Include(
                "~/content/kendo/2014.3.1314/kendo.common.min.css",
                "~/content/kendo/2014.3.1314/kendo.default.min.css",
                "~/content/kendo/2014.3.1314/kendo.rtl.min.css"
            ));
            #endregion

            #region Kendo GridView Js
            bundles.Add(new ScriptBundle("~/Scripts/kendo/en-US/js").Include(
                    "~/Scripts/kendo.mvc/kendo.web.min.js",
                    "~/Scripts/kendo.mvc/kendo.aspnetmvc.min.js"
                ));
            bundles.Add(new ScriptBundle("~/Scripts/kendo/fa-IR/js").Include(
                "~/Scripts/kendo.mvc/kendo.web.min.js",
                "~/Scripts/kendo.mvc/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo.mvc/cultures/kendo.culture.fa-IR.min.js",
                "~/Scripts/kendo/kendo.fa-IR.js"
            ));
            bundles.Add(new ScriptBundle("~/Scripts/kendo/ar-IQ/js").Include(
                "~/Scripts/kendo.mvc/kendo.web.min.js",
                "~/Scripts/kendo.mvc/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo.mvc/cultures/kendo.culture.ar-IQ.min.js",
                "~/Scripts/kendo/kendo.ar-IQ.js"
            ));
            #endregion

            bundles.Add(new ScriptBundle("~/persiaNumberJs").Include(
                "~/Plugins/persianumber-master/persianumber.min.js",
                "~/ClientTheme/js/custom/PersianNumberCustomScripts.js"));

            //client blog theme

            /*بررسی شود*/
            //bundles.Add(new StyleBundle("~/BlogListStyles")
            //        .Include("~/Content/assets/plugins/Select2_V4/css/select2.css", cssFixer)
            //        .Include("~/SiteClientTheme/custom/BlogCustomStyles.css", cssFixer)
            //);

            bundles.Add(
                new StyleBundle("~/bundles/maincss")
                    .Include("~/Content/bootstrap.css", cssFixer)
                    .Include("~/Content/bootstrap-responsive.css", cssFixer)
                    .Include("~/Content/my.css", cssFixer)
            );


            bundles.Add(new StyleBundle("~/bundles/blogcss")
                //.Include("~/SiteClientTheme/css/BlogMenuStyle.css", cssFixer)
           .Include("~/ClientTheme/libraries/bootstrap/RTL/css/bootstrap-rtl.min.css", cssFixer)
           .Include("~/ClientTheme/libraries/owl-carousel/owl.carousel.css", cssFixer)
           .Include("~/ClientTheme/libraries/owl-carousel/owl.theme.css", cssFixer)
           .Include("~/ClientTheme/libraries/flexslider/flexslider.css", cssFixer)
           .Include("~/ClientTheme/libraries/fonts/font-awesome.min.css", cssFixer)
           .Include("~/ClientTheme/libraries/animate/animate.min.css", cssFixer)
           .Include("~/ClientTheme/css/components.css", cssFixer)
           .Include("~/ClientTheme/css/style.css", cssFixer)
           .Include("~/ClientTheme/css/media.css", cssFixer)
           .Include("~/ClientTheme/css/RTL.css", cssFixer)
           .Include("~/ClientTheme/css/Blue.css", cssFixer)
           .Include("~/ClientTheme/css/CustomStyle.css", cssFixer)
           .Include("~/ClientTheme/css/fontiran.css", cssFixer)
           .Include("~/ClientTheme/css/IranianSans.css", cssFixer)
                //.Include("~/SiteClientTheme/css/megaMenu.css", cssFixer)
                //.Include("~/ClientTheme/css/menuColor.css", cssFixer)
           .Include("~/SiteClientTheme/css/font-awesome.css", cssFixer)
           .Include("~/SiteClientTheme/fonts/fi/flaticon.css", cssFixer)
                /*بررسی شود*/
           .Include("~/ClientTheme/css/Menu.css", cssFixer)
           .Include("~/ClientTheme/css/simple-sidebar.css", cssFixer)
           .Include("~/Content/toastr/toastr.min.css", cssFixer)
           .Include("~/Plugins/Rate-Yo-v2.2.0/jquery.rateyo.min.css", cssFixer)
           .Include("~/SiteClientTheme/custom/BlogCustomStyles.css", cssFixer)
           .Include("~/SiteClientTheme/custom/custom-footer.css", cssFixer)
           .Include("~/ClientTheme/css/Footer.css", cssFixer)
           );

            /*بررسی شود*/
            bundles.Add(new ScriptBundle("~/BlogLayoutTopJs").Include(
                "~/ClientTheme/libraries/jquery.min.js",
                "~/Scripts/toastr/toastr.min.js"              
                ));

            bundles.Add(new ScriptBundle("~/bundles/blogcjs").Include(
                //"~/ClientTheme/libraries/jquery.min.js",
                "~/ClientTheme/libraries/jquery.easing.min.js",
                "~/ClientTheme/libraries/bootstrap/RTL/js/bootstrap-rtl.min.js",
                "~/ClientTheme/libraries/jquery.animateNumber.min.js",
                "~/ClientTheme/libraries/jquery.appear.js",
                "~/ClientTheme/libraries/jquery.knob.js",

                "~/ClientTheme/libraries/wow.min.js",
                "~/ClientTheme/libraries/flexslider/jquery.flexslider.js", /*-min*/
                "~/ClientTheme/libraries/owl-carousel/owl.carousel.min.js",
                "~/SiteClientTheme/js/owl.carousel.js", /*قبلا نبود افزودیم*/
                "~/ClientTheme/libraries/expanding-search/modernizr.custom.js",

                "~/ClientTheme/libraries/expanding-search/classie.js",
                "~/ClientTheme/libraries/jssor.js",
                "~/ClientTheme/libraries/jssor.slider.js",
                //"~/ClientTheme/libraries/jquery.marquee.js",
                "~/ClientTheme/libraries/Marquee/jquery.marquee.min.js",
                "~/ClientTheme/js/functions.js"
                //بررسی شود
                //"~/Plugins/cool-share/plugin.js",
                //"~/Plugins/cool-share/shareconfig.js"
            ));

            //TourDetail ClientCss
            bundles.Add(new StyleBundle("~/bundles/TourDetailCss")
                .Include("~/content/charteran/libs/fancybox-master/dist/jquery.fancybox.css", cssFixer)
                .Include("~/content/charteran/libs/PgwSlider-master/pgwslider.css", cssFixer)
            );

            //Home Custom ClientCss
            bundles.Add(new StyleBundle("~/bundles/HomeCustomClientCss")
                .Include("~/ClientTheme/libraries/bootstrap/RTL/css/bootstrap-rtl.min.css", cssFixer)
                .Include("~/SiteClientTheme/custom/HomeCustomCSS.css", cssFixer)
                .Include("~/ClientTheme/css/style.css", cssFixer)
                .Include("~/SiteClientTheme/custom/SliderCustomCss.css", cssFixer)
                .Include("~/Plugins/slimscroll/prettify.css", cssFixer)
                   );

            //Blog Styles
            bundles.Add(new StyleBundle("~/bundles/BlogClientCss")
           .Include("~/ClientTheme/Blog/css/reset.css", cssFixer)
           .Include("~/clienttheme/blog/css/font.css", cssFixer)
           .Include("~/clienttheme/blog/css/xl.css", cssFixer)
           .Include("~/clienttheme/blog/css/l.css", cssFixer)
           .Include("~/clienttheme/blog/css/m.css", cssFixer)
           .Include("~/clienttheme/blog/css/s.css", cssFixer)
           .Include("~/Content/toastr/toastr.min.css", cssFixer)
           );

            //cLient theme
            bundles.Add(new StyleBundle("~/bundles/ClientCss")
            .Include("~/content/charteran/libs/jquery-nice-select/css/nice-select-rtl.css", cssFixer)
            .Include("~/content/charteran/libs/bootstrap/dist/css/bootstrap.min.css", cssFixer)
            .Include("~/content/charteran/libs/bootstrap/dist/css/bootstrap-rtl.min.css", cssFixer)
            .Include("~/content/charteran/libs/font-awesome/css/font-awesome.min.css", cssFixer)
            .Include("~/content/charteran/libs/owl.carousel/dist/assets/owl.carousel.css", cssFixer)
            .Include("~/content/charteran/libs/owl.carousel/dist/assets/owl.transitions.css", cssFixer)
            .Include("~/content/charteran/libs/animate.css/animate.min.css", cssFixer)
            .Include("~/content/charteran/libs/normalize.css/normalize.css", cssFixer)
            .Include("~/Content/assets/plugins/Select2_V4/css/select2.css", cssFixer)
            .Include("~/content/charteran/assets/css/main.css", cssFixer)
            .Include("~/Content/toastr/toastr.min.css", cssFixer)
            );

            bundles.Add(new ScriptBundle("~/bundles/CustomerScripts")
               .Include("~/Scripts/nets/jquery-2.0.3.min.js")
                //.Include("~/Plugins/momentDate/moment.min.js")
                //.Include("~/Plugins/momentDate/moment-jalaali.js")
               .Include("~/Scripts/jquery.unobtrusive-ajax.min.js")
               .Include("~/Scripts/jquery.validate.js")
               .Include("~/Scripts/jquery.validate.unobtrusive.js")
               );

            bundles.Add(new ScriptBundle("~/bundles/Clientjqueryjs")
                //.Include("~/SiteClientTheme/js/jquery.min.js")
                .Include("~/Scripts/nets/jquery-2.0.3.min.js")
                );

            bundles.Add(new ScriptBundle("~/HomeCustomScripts")
                .Include("~/SiteClientTheme/js/custom/HomeCustomScripts.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/TopClientjs").Include(
                //"~/Scripts/jquery-1.10.2.min.js",
                //"~/Scripts/jquery-ui-1.12.0.min.js",
                //"~/content/charteran/libs/bootstrap/dist/js/bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Clientjs").Include(
                //"~/content/charteran/libs/jquery/dist/jquery.min.js",
                //"~/content/charteran/libs/bootstrap/dist/js/bootstrap.min.js",
               // "~/content/charteran/libs/owl.carousel/dist/owl.carousel.min.js",
                 //   "~/content/charteran/libs/wowjs/dist/wow.min.js",
                 //   "~/Content/charteran/libs/sticky-sidebar-master/dist/jquery.sticky-sidebar.min.js",
                 //   "~/content/charteran/libs/fancybox-master/dist/jquery.fancybox.min.js",
                //    "~/content/charteran/libs/kl-scripts/kl-scripts.js",
                 //   "~/Content/assets/plugins/Select2_V4/js/select2.js",
                //"~/content/charteran/libs/jquery-nice-select/js/jquery.nice-select.js",
                //    "~/content/charteran/assets/js/custom.js",
                    "~/Scripts/toastr/toastr.min.js"
            ));
        }
    }
}