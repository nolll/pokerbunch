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
                "Site Routes with user name",
                "-/{controller}/{action}/{userName}",
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
                "Bunch Routes with player name",
                "{slug}/{controller}/{action}/{playerName}"
            );

            routes.MapRoute(
                "Bunch Routes with date and player name",
                "{slug}/{controller}/{action}/{dateStr}/{playerName}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}" }
            );

            routes.MapRoute(
                "Bunch Routes with date, player name and checkpoint id",
                "{slug}/{controller}/{action}/{dateStr}/{playerName}/{checkpointId}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}", checkpointId = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Bunch Routes",
                "{slug}/{controller}/{action}"
            );

        }
    }
}