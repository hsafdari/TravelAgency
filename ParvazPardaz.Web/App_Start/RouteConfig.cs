using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ParvazPardaz.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ParvazPardaz.Web.Controllers" }
            );

            routes.MapRoute(
               name: "ProductPage",
               url: "{controller}/{action}/{id}/{productTitle}",
               defaults: new { controller = "Product", action = "ProductDetail", productTitle = UrlParameter.Optional },
               namespaces: new string[] { "ParvazPardaz.Web.Controllers" }
           );



            routes.MapRoute(
              name: "PostDetail",
              url: "{controller}/{action}/{id}/{postTitle}",
              defaults: new { controller = "Post", action = "PostDetail", postTitle = UrlParameter.Optional },
              namespaces: new string[] { "ParvazPardaz.Web.Controllers" }
          );

            routes.MapRoute(
                  name: "Home",
                  url: "{lang}",
                  defaults: new { controller = "Home", action = "Index", lang = UrlParameter.Optional },
                  namespaces: new string[] { "ParvazPardaz.Web.Controllers" }
              );
            routes.MapRoute(
                  name: "ShoppingBasket",
                  url: "{controller}/{action}/{idPro}",
                  defaults: new { controller = "Home", action = "ShoppingBasket", lang = UrlParameter.Optional, idPro = UrlParameter.Optional },
                  namespaces: new string[] { "ParvazPardaz.Web.Controllers" }
              );

            routes.MapRoute(
                name: "admin",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ParvazPardaz.Web.Areas.Admin.Controllers" }
                );

        }
    }
}
