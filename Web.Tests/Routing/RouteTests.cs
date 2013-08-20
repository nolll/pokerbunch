using MvcRouteUnitTester;
using NUnit.Framework;
using Web;

public class RouteTests
{
    [Test]
    public void TestIncomingRoutes()
    {
        var tester = new RouteTester<MvcApplication>();

        //tester.WithIncomingRequest("/-/auth/login").ShouldMatchRoute("Auth", "Login", new { gameName="-" });
        tester.WithIncomingRequest("/gamename/cashgame/index").ShouldMatchRoute("Cashgame", "Index", new { gameName = "gamename" });
        //tester.WithIncomingRequest("/home/index").ShouldMatchRoute("Home", "Index");
    }
}