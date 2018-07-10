using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WBrand.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            //    name: "cms",
            //    url: "cms-system/{action}",
            //    defaults: new { controller = "Admin", action = "Index", alias = UrlParameter.Optional },
            //    namespaces: new string[] { "WBrand.Web.Controllers" }
            //);
            routes.MapRoute(
                name: "cms-systems",
                url: "cms-system",
                defaults: new { controller = "Admin", action = "system", alias = UrlParameter.Optional },
                namespaces: new string[] { "WBrand.Web.Controllers" }
            );
            routes.MapRoute(
                name: "cms",
                url: "cms-login",
                defaults: new { controller = "Admin", action = "login-cms", alias = UrlParameter.Optional },
                namespaces: new string[] { "WBrand.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "system", id = UrlParameter.Optional }
            );
        }
    }
}
