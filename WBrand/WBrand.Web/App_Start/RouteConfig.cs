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


            routes.MapRoute(
                name: "blog",
                url: "blog/{p}/{cat}/{filter}",
                defaults: new { controller = "Blog", action = "Index", p = UrlParameter.Optional, cat = UrlParameter.Optional, filter = UrlParameter.Optional },
                namespaces: new string[] { "NBT.Web.Controllers" }
            );
            routes.MapRoute(
                name: "blogDetail",
                url: "blog/{slug}.html",
                constraints: new { slug = @"^([a-z0-9]+\-)*[a-z0-9]+$" },
                defaults: new { controller = "Blog", action = "Detail", slug = UrlParameter.Optional },
                namespaces: new string[] { "NBT.Web.Controllers" }
            );
            routes.MapRoute(
                name: "product",
                url: "product/{p}/{cat}/{filter}",
                defaults: new { controller = "Product", action = "Index", p = UrlParameter.Optional, cat = UrlParameter.Optional, filter = UrlParameter.Optional },
                namespaces: new string[] { "NBT.Web.Controllers" }
            );
            routes.MapRoute(
                name: "productDetail",
                url: "product/{slug}.html",
                constraints: new { slug = @"^([a-z0-9]+\-)*[a-z0-9]+$" },
                defaults: new { controller = "Product", action = "Detail", slug = UrlParameter.Optional },
                namespaces: new string[] { "NBT.Web.Controllers" }
            );
            routes.MapRoute(
                name: "contact",
                url: "contact",
                defaults: new { controller = "Home", action = "Contact", slug = UrlParameter.Optional },
                namespaces: new string[] { "NBT.Web.Controllers" }
            );
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
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
