using MvcRouteUnitTester;
using NUnit.Framework;
using Web;

public class RouteTests
{
    [Test]
    public void TestIncomingRoutes()
    {
        var tester = new RouteTester<MvcApplication>();

        //tester.WithIncomingRequest("/-/auth/login").ShouldMatchRoute("Auth", "Login");
        //tester.WithIncomingRequest("/-/homegame/listing").ShouldMatchRoute("Homegame", "Listing");
        //tester.WithIncomingRequest("/bunch/cashgame/index").ShouldMatchRoute("Cashgame", "Index", new { gameName = "bunch" });
        //tester.WithIncomingRequest("/bunch/cashgame/matrix/1").ShouldMatchRoute("Cashgame", "Matrix", new { gameName = "bunch", year = 1 });
        //tester.WithIncomingRequest("/").ShouldMatchRoute("Home", "Index");
    }
}