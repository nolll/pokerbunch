using MvcRouteUnitTester;
using NUnit.Framework;

namespace Web.Tests.Routing
{
    public class RouteTests
    {
        private const string ExpectedUser = "username";
        private const string ExpectedBunch = "bunchname";
        private const string ExpectedDate = "2001-01-01";
        private const string ExpectedPlayer = "playername";
        private const int ExpectedYear = 2000;

        private readonly RouteTester<MvcApplication> _tester;

        public RouteTests()
        {
            _tester = new RouteTester<MvcApplication>();
        }

        [Test]
        public void SiteRoutes_WithNoParams()
        {
            _tester.WithIncomingRequest("/").ShouldMatchRoute("Home", "Index");
            _tester.WithIncomingRequest("/-/auth/login").ShouldMatchRoute("Auth", "Login");
            _tester.WithIncomingRequest("/-/auth/logout").ShouldMatchRoute("Auth", "Logout");
            _tester.WithIncomingRequest("/-/homegame/listing").ShouldMatchRoute("Homegame", "Listing");
            _tester.WithIncomingRequest("/-/homegame/add").ShouldMatchRoute("Homegame", "Add");
            _tester.WithIncomingRequest("/-/homegame/created").ShouldMatchRoute("Homegame", "Created");
        }

        [Test]
        public void SiteRoutes_WithNameParam()
        {
            _tester.WithIncomingRequest("/-/user/details/username").ShouldMatchRoute("User", "Details", new { name = ExpectedUser });
        }

        [Test]
        public void BunchRoutes_WithNoParams()
        {
            _tester.WithIncomingRequest("/bunchname/homegame/details").ShouldMatchRoute("Homegame", "Details", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/player/index").ShouldMatchRoute("Player", "Index", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/index").ShouldMatchRoute("Cashgame", "Index", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/add").ShouldMatchRoute("Cashgame", "Add", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/running").ShouldMatchRoute("Cashgame", "Running", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/matrix").ShouldMatchRoute("Cashgame", "Matrix", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/facts").ShouldMatchRoute("Cashgame", "Facts", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/leaderboard").ShouldMatchRoute("Cashgame", "Leaderboard", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/listing").ShouldMatchRoute("Cashgame", "Listing", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/chart").ShouldMatchRoute("Cashgame", "Chart", new { gameName = ExpectedBunch });
            _tester.WithIncomingRequest("/bunchname/cashgame/chartjson").ShouldMatchRoute("Cashgame", "ChartJson", new { gameName = ExpectedBunch });
        }

        [Test]
        public void BunchRoutes_WithNameParam()
        {
            _tester.WithIncomingRequest("/bunchname/player/details/playername").ShouldMatchRoute("Player", "Details", new { gameName = ExpectedBunch, name = ExpectedPlayer });
            _tester.WithIncomingRequest("/bunchname/player/delete/playername").ShouldMatchRoute("Player", "Delete", new { gameName = ExpectedBunch, name = ExpectedPlayer });
            _tester.WithIncomingRequest("/bunchname/cashgame/buyin/playername").ShouldMatchRoute("Cashgame", "Buyin", new { gameName = ExpectedBunch, name = ExpectedPlayer });
        }

        [Test]
        public void BunchRouts_WithYearParam()
        {
            _tester.WithIncomingRequest("/bunchname/cashgame/matrix/2000").ShouldMatchRoute("Cashgame", "Matrix", new { gameName = ExpectedBunch, year = ExpectedYear });
            _tester.WithIncomingRequest("/bunchname/cashgame/facts/2000").ShouldMatchRoute("Cashgame", "Facts", new { gameName = ExpectedBunch, year = ExpectedYear });
            _tester.WithIncomingRequest("/bunchname/cashgame/leaderboard/2000").ShouldMatchRoute("Cashgame", "Leaderboard", new { gameName = ExpectedBunch, year = ExpectedYear });
            _tester.WithIncomingRequest("/bunchname/cashgame/listing/2000").ShouldMatchRoute("Cashgame", "Listing", new { gameName = ExpectedBunch, year = ExpectedYear });
            _tester.WithIncomingRequest("/bunchname/cashgame/chart/2000").ShouldMatchRoute("Cashgame", "Chart", new { gameName = ExpectedBunch, year = ExpectedYear });
            _tester.WithIncomingRequest("/bunchname/cashgame/chartjson/2000").ShouldMatchRoute("Cashgame", "ChartJson", new { gameName = ExpectedBunch, year = ExpectedYear });
        }

        [Test]
        public void BunchRoutes_WithDateParam()
        {
            _tester.WithIncomingRequest("/bunchname/cashgame/details/2001-01-01").ShouldMatchRoute("Cashgame", "Details", new { gameName = ExpectedBunch, dateStr = ExpectedDate });
            _tester.WithIncomingRequest("/bunchname/cashgame/detailschartjson/2001-01-01").ShouldMatchRoute("Cashgame", "DetailsChartJson", new { gameName = ExpectedBunch, dateStr = ExpectedDate });
        }

        [Test]
        public void BunchRoutes_WithDateAndNameParams()
        {
            _tester.WithIncomingRequest("/bunchname/cashgame/action/2001-01-01/playername").ShouldMatchRoute("Cashgame", "Action", new { gameName = ExpectedBunch, dateStr = ExpectedDate, name = ExpectedPlayer });
            _tester.WithIncomingRequest("/bunchname/cashgame/actionchartjson/2001-01-01/playername").ShouldMatchRoute("Cashgame", "ActionChartJson", new { gameName = ExpectedBunch, dateStr = ExpectedDate, name = ExpectedPlayer });
        }
    }
}