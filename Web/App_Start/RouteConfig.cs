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
                new { action = "Index" }
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
                "{slug}/{controller}/{action}/{year}",
                new {  },
                new { year = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Bunch Routes with date",
                "{slug}/{controller}/{action}/{dateStr}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}" }
            );

            routes.MapRoute(
                "Bunch Routes with name",
                "{slug}/{controller}/{action}/{name}"
            );

            routes.MapRoute(
                "Bunch Routes with date and name",
                "{slug}/{controller}/{action}/{dateStr}/{name}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}" }
            );

            routes.MapRoute(
                "Bunch Routes with date, name and id",
                "{slug}/{controller}/{action}/{dateStr}/{name}/{id}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}", id = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Bunch Routes",
                "{slug}/{controller}/{action}"
            );

        }
    }
}