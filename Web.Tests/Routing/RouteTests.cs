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
        //tester.WithIncomingRequest("/").ShouldMatchRoute("Home", "Index");
        //tester.WithIncomingRequest("/auth/login").ShouldMatchRoute("Auth", "Login");
    }
}