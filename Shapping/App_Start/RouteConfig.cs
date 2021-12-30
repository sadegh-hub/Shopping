using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shapping
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{Groupkalas}/{Subgroupkalas}/{id}/{productname}",
                defaults: new { controller = "Home", action = "Index", Groupkalas="1", Subgroupkalas= "1", id = UrlParameter.Optional,productname= UrlParameter.Optional }
            );
        }
    }
}
