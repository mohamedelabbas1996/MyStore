using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               null,
               "",
                new { controller = "Products", action = "List",
                category = (string)null,page=1}
                );


            routes.MapRoute(
                null,
                "Page{page}",
                new {controller="Products", action="List",category = (string)null },
                new {page= @"\d+"}
                
                );

            routes.MapRoute(null,
                "{category}",
                new { controller="Products", action="List",page=1}  
                );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Products", action = "List" },
                new { page = @"\d+" }
                );
            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Products", action = "List" }
            );
            routes.MapRoute(
                null,
                "{controller}/{action}"
               
            );
        }
    }
}
