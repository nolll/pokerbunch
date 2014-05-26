using MvcRouteUnitTester;
using NUnit.Framework;
using Web;

namespace Tests.Web.Routing
{
    public class RouteTests
    {
        private const string ExpectedUser = "username";
        private const string ExpectedBunch = "bunchname";
        private const string ExpectedDate = "2001-01-01";
        private const int ExpectedPlayer = 1;
        private const int ExpectedYear = 2000;

        private readonly RouteTester<MvcApplication> _tester;

        public RouteTests()
        {
            _tester = new RouteTester<MvcApplication>();
        }

        [TestCase("/", "Home", "Index")]
        [TestCase("/-/auth/login", "Auth", "Login")]
        [TestCase("/-/auth/logout", "Auth", "Logout")]
        [TestCase("/-/homegame/list", "Homegame", "List")]
        [TestCase("/-/homegame/add", "Homegame", "Add")]
        [TestCase("/-/homegame/created", "Homegame", "Created")]
        public void SiteRoutes_WithNoParams(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action);
        }

        [TestCase("/-/user/details/username", "User", "Details")]
        public void SiteRoutes_WithUserNameParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { userName = ExpectedUser });
        }

        [TestCase("/bunchname/homegame/details", "Homegame", "Details")]
        [TestCase("/bunchname/homegame/edit", "Homegame", "Edit")]
        [TestCase("/bunchname/player/index", "Player", "Index")]
        [TestCase("/bunchname/player/add", "Player", "Add")]
        [TestCase("/bunchname/player/created", "Player", "Created")]
        [TestCase("/bunchname/cashgame/index", "Cashgame", "Index")]
        [TestCase("/bunchname/cashgame/add", "Cashgame", "Add")]
        [TestCase("/bunchname/cashgame/running", "Cashgame", "Running")]
        [TestCase("/bunchname/cashgame/matrix", "Cashgame", "Matrix")]
        [TestCase("/bunchname/cashgame/facts", "Cashgame", "Facts")]
        [TestCase("/bunchname/cashgame/toplist", "Cashgame", "Toplist")]
        [TestCase("/bunchname/cashgame/list", "Cashgame", "List")]
        [TestCase("/bunchname/cashgame/chart", "Cashgame", "Chart")]
        [TestCase("/bunchname/cashgame/chartjson", "Cashgame", "ChartJson")]
        public void BunchRoutes_WithNoParams(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { slug = ExpectedBunch });
        }

        [TestCase("/bunchname/player/details/1", "Player", "Details")]
        [TestCase("/bunchname/player/delete/1", "Player", "Delete")]
        [TestCase("/bunchname/cashgame/buyin/1", "Cashgame", "Buyin")]
        [TestCase("/bunchname/cashgame/report/1", "Cashgame", "Report")]
        [TestCase("/bunchname/cashgame/cashout/1", "Cashgame", "Cashout")]
        public void BunchRoutes_WithPlayerIdParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { slug = ExpectedBunch, playerId = ExpectedPlayer });
        }

        [TestCase("/bunchname/cashgame/matrix/2000", "Cashgame", "Matrix")]
        [TestCase("/bunchname/cashgame/facts/2000", "Cashgame", "Facts")]
        [TestCase("/bunchname/cashgame/toplist/2000", "Cashgame", "Toplist")]
        [TestCase("/bunchname/cashgame/list/2000", "Cashgame", "List")]
        [TestCase("/bunchname/cashgame/chart/2000", "Cashgame", "Chart")]
        [TestCase("/bunchname/cashgame/chartjson/2000", "Cashgame", "ChartJson")]
        public void BunchRouts_WithYearParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { slug = ExpectedBunch, year = ExpectedYear });
        }

        [TestCase("/bunchname/cashgame/details/2001-01-01", "Cashgame", "Details")]
        [TestCase("/bunchname/cashgame/detailschartjson/2001-01-01", "Cashgame", "DetailsChartJson")]
        public void BunchRoutes_WithDateParam(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { slug = ExpectedBunch, dateStr = ExpectedDate });
        }

        [TestCase("/bunchname/cashgame/action/2001-01-01/1", "Cashgame", "Action")]
        [TestCase("/bunchname/cashgame/actionchartjson/2001-01-01/1", "Cashgame", "ActionChartJson")]
        public void BunchRoutes_WithDateAndPlayerIdParams(string url, string controller, string action)
        {
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { slug = ExpectedBunch, dateStr = ExpectedDate, playerId = ExpectedPlayer });
        }

        [Test]
        public void DeleteCheckpointRoute_WithDateAndPlayerIdAndCheckpointIdParams()
        {
            const int expectedId = 2;
            const string url = "/bunchname/cashgame/deletecheckpoint/2001-01-01/1/2";
            const string controller = "Cashgame";
            const string action = "DeleteCheckpoint";
            _tester.WithIncomingRequest(url).ShouldMatchRoute(controller, action, new { slug = ExpectedBunch, dateStr = ExpectedDate, playerId = ExpectedPlayer, checkpointId = expectedId });
        }
    }
}