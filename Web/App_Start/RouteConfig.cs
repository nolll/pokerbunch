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
                "Player Details",
                "{slug}/player/{action}/{playerId}",
                new { controller = "Player", action = "Details" },
                new { playerId = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Buyin",
                "{slug}/cashgame/buyin/{playerId}",
                new { controller = "Cashgame", action = "Buyin" },
                new { playerId = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Report",
                "{slug}/cashgame/report/{playerId}",
                new { controller = "Cashgame", action = "Report" },
                new { playerId = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Cashout",
                "{slug}/cashgame/cashout/{playerId}",
                new { controller = "Cashgame", action = "Cashout" },
                new { playerId = @"^[0-9]+$" }
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
                "Bunch Routes with date and player id",
                "{slug}/{controller}/{action}/{dateStr}/{playerId}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}", playerId = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Bunch Routes with date, player id and checkpoint id",
                "{slug}/{controller}/{action}/{dateStr}/{playerId}/{checkpointId}",
                new { },
                new { dateStr = @"\d{4}-\d{2}-\d{2}", playerId = @"^[0-9]+$", checkpointId = @"^[0-9]+$" }
            );

            routes.MapRoute(
                "Bunch Routes",
                "{slug}/{controller}/{action}"
            );

        }
    }
}