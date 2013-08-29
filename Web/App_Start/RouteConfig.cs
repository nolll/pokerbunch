using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Auth Routes",
                "-/auth/{action}",
                new { controller = "Auth" }
            );

            routes.MapRoute(
                "Homegame Listing Routes",
                "-/homegame/{action}",
                new { controller = "Homegame" }
            );

            routes.MapRoute(
                "Homegame Routes",
                "{gamename}/homegame/{action}",
                new { controller = "Homegame" }
            );

            routes.MapRoute(
                "Cashgame Routes",
                "{gamename}/cashgame/{action}/{year}",
                new { controller = "Cashgame", year = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Player Routes",
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