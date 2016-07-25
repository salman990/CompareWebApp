using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompareWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Compare", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Category",
                url: "{controller}/{action}/{categoryName}/{currentPageIndex}",
                defaults: new { controller = "Category", action = "Index", categoryName = UrlParameter.Optional, currentPageIndex = UrlParameter.Optional }
            );
        }
    }
}
