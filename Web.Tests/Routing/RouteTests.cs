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

        [TestCase("/", "Home", "Index")]
        [TestCase("/-/auth/login", "Auth", "Login")]
        [TestCase("/-/auth/logout", "Auth", "Logout")]
        [TestCase("/-/homegame/listing", "Homegame", "Listing")]
        [TestCase("/-/homegame/add", "Homegame", "Add")]
        [TestCase("/-/homegame/created", "Homegame", "Created")]
        public void SiteRoutes_WithNoParams(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action);
        }

        [TestCase("/-/user/details/username", "User", "Details")]
        public void SiteRoutes_WithNameParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { name = ExpectedUser });
        }

        [TestCase("/bunchname/homegame/details", "Homegame", "Details")]
        [TestCase("/bunchname/homegame/edit", "Homegame", "Edit")]
        [TestCase("/bunchname/player/index", "Player", "Index")]
        [TestCase("/bunchname/cashgame/index", "Cashgame", "Index")]
        [TestCase("/bunchname/cashgame/add", "Cashgame", "Add")]
        [TestCase("/bunchname/cashgame/running", "Cashgame", "Running")]
        [TestCase("/bunchname/cashgame/matrix", "Cashgame", "Matrix")]
        [TestCase("/bunchname/cashgame/facts", "Cashgame", "Facts")]
        [TestCase("/bunchname/cashgame/leaderboard", "Cashgame", "Leaderboard")]
        [TestCase("/bunchname/cashgame/listing", "Cashgame", "Listing")]
        [TestCase("/bunchname/cashgame/chart", "Cashgame", "Chart")]
        [TestCase("/bunchname/cashgame/chartjson", "Cashgame", "ChartJson")]
        public void BunchRoutes_WithNoParams(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { gameName = ExpectedBunch });
        }

        [TestCase("/bunchname/player/details/playername", "Player", "Details")]
        [TestCase("/bunchname/player/delete/playername", "Player", "Delete")]
        [TestCase("/bunchname/cashgame/buyin/playername", "Cashgame", "Buyin")]
        [TestCase("/bunchname/cashgame/report/playername", "Cashgame", "Report")]
        public void BunchRoutes_WithNameParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { gameName = ExpectedBunch, name = ExpectedPlayer });
        }

        [TestCase("/bunchname/cashgame/matrix/2000", "Cashgame", "Matrix")]
        [TestCase("/bunchname/cashgame/facts/2000", "Cashgame", "Facts")]
        [TestCase("/bunchname/cashgame/leaderboard/2000", "Cashgame", "Leaderboard")]
        [TestCase("/bunchname/cashgame/listing/2000", "Cashgame", "Listing")]
        [TestCase("/bunchname/cashgame/chart/2000", "Cashgame", "Chart")]
        [TestCase("/bunchname/cashgame/chartjson/2000", "Cashgame", "ChartJson")]
        public void BunchRouts_WithYearParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { gameName = ExpectedBunch, year = ExpectedYear });
        }

        [TestCase("/bunchname/cashgame/details/2001-01-01", "Cashgame", "Details")]
        [TestCase("/bunchname/cashgame/detailschartjson/2001-01-01", "Cashgame", "DetailsChartJson")]
        public void BunchRoutes_WithDateParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { gameName = ExpectedBunch, dateStr = ExpectedDate });
        }

        [TestCase("/bunchname/cashgame/action/2001-01-01/playername", "Cashgame", "Action")]
        [TestCase("/bunchname/cashgame/actionchartjson/2001-01-01/playername", "Cashgame", "ActionChartJson")]
        public void BunchRoutes_WithDateAndNameParams(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { gameName = ExpectedBunch, dateStr = ExpectedDate, name = ExpectedPlayer });
        }

        [Test]
        public void DeleteCheckpointRoute_WithDateAndNameAndIdParams()
        {
            var expectedId = 1;
            var url = "/bunchname/cashgame/deletecheckpoint/2001-01-01/playername/1";
            var controller = "Cashgame";
            var action = "DeleteCheckpoint";
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { gameName = ExpectedBunch, dateStr = ExpectedDate, name = ExpectedPlayer, id = expectedId });
        }
    }
}