using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                "CashgameIndex",
                "{gameName}/cashgame/index",
                new { controller = "Cashgame", action = "Index", gameName = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Login",
                "-/auth/Login",
                new { controller = "Auth", action = "Login", gameName = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}