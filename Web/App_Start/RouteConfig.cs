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
                "Auth",
                "-/auth/{action}",
                new { controller = "Auth" }
            );

            routes.MapRoute(
                "Homegame Actions",
                "-/homegame/{action}",
                new { controller = "Homegame" }
            );

            routes.MapRoute(
                "User",
                "-/user/{action}/{userName}",
                new { controller = "User", userName = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Homegame",
                "{gamename}/homegame/{action}",
                new { controller = "Homegame" }
            );

            routes.MapRoute(
                "Cashgame Add",
                "{gamename}/cashgame/add",
                new { controller = "Cashgame", action = "Add" }
            );

            routes.MapRoute(
                "Cashgame Running",
                "{gamename}/cashgame/running",
                new { controller = "Cashgame", action = "Running" }
            );

            routes.MapRoute(
                "Cashgame Details",
                "{gamename}/cashgame/details/{datestr}",
                new { controller = "Cashgame", action = "Details", dateStr = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Cashgame Details Chart",
                "{gamename}/cashgame/detailschartjson/{datestr}",
                new { controller = "Cashgame", action = "DetailsChartJson", dateStr = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Cashgame",
                "{gamename}/cashgame/{action}/{year}",
                new { controller = "Cashgame", year = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Player",
                "{gamename}/player/{action}/{playerName}",
                new { controller = "Player", playerName = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Home",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}