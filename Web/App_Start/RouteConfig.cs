using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Site Routes",
                "-/{controller}/{action}",
                new { }
            );

            routes.MapRoute(
                "Site Routes with name",
                "-/{controller}/{action}/{name}",
                new {  }
            );

            routes.MapRoute(
                "Home",
                "",
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "Bunch Routes with year",
                "{gameName}/{controller}/{action}/{year}",
                new {  },
                new { year = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Bunch Routes with date",
                "{gameName}/{controller}/{action}/{dateStr}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}" }
            );

            routes.MapRoute(
                "Bunch Routes with name",
                "{gameName}/{controller}/{action}/{name}"
            );

            routes.MapRoute(
                "Bunch Routes with date and name",
                "{gameName}/{controller}/{action}/{dateStr}/{name}",
                new { }
            );

            routes.MapRoute(
                "Bunch Routes",
                "{gameName}/{controller}/{action}"
            );

        }
    }
}