using System.Web.Mvc;

namespace ParvazPardaz.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                 "Admin_tourPreview",
                 "Admin/Tour/TourPreview/{url}",
                 new { controller = "Tour", action = "TourPreview", url = UrlParameter.Optional }
             );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}