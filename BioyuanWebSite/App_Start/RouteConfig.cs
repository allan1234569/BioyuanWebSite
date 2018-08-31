using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "route2",
                url: "{controller}/About/{action}",
                defaults: new { controller = "Home", action = "AboutUs" },
                namespaces: new string[] { "MvcApplication1.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MvcApplication1.Controllers" }
            );

            routes.MapRoute(
                name: "route1",
                url: "{controller}/Products/{id}",
                defaults: new { controller = "Home", action = "Products", id = UrlParameter.Optional },
                namespaces: new string[] { "MvcApplication1.Controllers" }
            );

            routes.MapRoute(
                name: "route3",
                url: "{controller}/{action}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MvcApplication1.Controllers" }
            );
        }
    }
}