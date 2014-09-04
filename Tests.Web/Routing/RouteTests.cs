using System.Net.Http;
using System.Web.Routing;
using Core.Entities;
using MvcRouteTester;
using NUnit.Framework;
using Web;
using Web.Controllers;
using Web.Models.AuthModels;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Report;

namespace Tests.Web.Routing
{
    public class RouteTests
    {
        private readonly RouteCollection _routes;

        public RouteTests()
        {
            _routes = RouteTable.Routes;
            _routes.Clear();
            _routes.MapAttributeRoutesInAssembly(typeof(HomeController).Assembly);
            RouteConfig.RegisterRoutes(_routes);
        }

        [Test]
        public void Home()
        {
            _routes.ShouldMap("/").To<HomeController>(x => x.Index());
        }

        [Test]
        public void Login()
        {
            _routes.ShouldMap("/-/auth/login").To<AuthController>(x => x.Login(null));
            _routes.ShouldMap("/-/auth/login?returnUrl=a").To<AuthController>(x => x.Login("a"));
            _routes.ShouldMap("/-/auth/login").To<AuthController>(HttpMethod.Post, x => x.Login_Post(new LoginPostModel()));
        }

        [Test]
        public void Logout()
        {
            _routes.ShouldMap("/-/auth/logout").To<AuthController>(x => x.Logout());
        }

        [Test]
        public void UserDetailds()
        {
            _routes.ShouldMap("/-/user/details/a").To<UserController>(x => x.Details("a"));
        }

        [Test]
        public void BunchDetails()
        {
            _routes.ShouldMap("/a/homegame/details").To<HomegameController>(x => x.Details("a"));
        }

        [Test]
        public void EditBunch()
        {
            _routes.ShouldMap("/a/homegame/edit").To<HomegameController>(x => x.Edit("a"));
        }

        [Test]
        public void BunchList()
        {
            _routes.ShouldMap("/-/homegame/list").To<HomegameController>(x => x.List());
        }

        [Test]
        public void AddBunch()
        {
            _routes.ShouldMap("/-/homegame/add").To<HomegameController>(x => x.Add());
        }

        [Test]
        public void AddBunchConfirmation()
        {
            _routes.ShouldMap("/-/homegame/created").To<HomegameController>(x => x.Created());
        }

        [Test]
        public void PlayerIndex()
        {
            _routes.ShouldMap("/a/player/index").To<PlayerController>(x => x.Index("a"));
        }

        [Test]
        public void AddPlayerRoute()
        {
            _routes.ShouldMap("/a/player/add").To<PlayerController>(x => x.Add("a"));
        }

        [Test]
        public void AddPlayerConfirmation()
        {
            _routes.ShouldMap("/a/player/created").To<PlayerController>(x => x.Created("a"));
        }

        [Test]
        public void PlayerDetails()
        {
            _routes.ShouldMap("/a/player/details/1").To<PlayerController>(x => x.Details("a", 1));
        }

        [Test]
        public void DeletePlayer()
        {
            _routes.ShouldMap("/a/player/delete/1").To<PlayerController>(x => x.Delete("a", 1));
        }

        [Test]
        public void InvitePlayer()
        {
            _routes.ShouldMap("/a/player/invite/1").To<PlayerController>(x => x.Invite("a", 1));
        }

        [Test]
        public void InvitePlayerConfirmation()
        {
            _routes.ShouldMap("/a/player/invited/1").To<PlayerController>(x => x.Invited("a", 1));
        }

        [Test]
        public void CashgameIndex()
        {
            _routes.ShouldMap("/a/cashgame/index").To<CashgameController>(x => x.Index("a"));
        }

        [Test]
        public void AddCashgame()
        {
            _routes.ShouldMap("/a/cashgame/add").To<CashgameController>(x => x.Add("a"));
            _routes.ShouldMap("/a/cashgame/add").To<CashgameController>(HttpMethod.Post, x => x.Add_Post("a", new AddCashgamePostModel()));
        }

        [Test]
        public void EditCashgame()
        {
            _routes.ShouldMap("/a/cashgame/edit/2000-01-01").To<CashgameController>(x => x.Edit("a", "2000-01-01"));
            _routes.ShouldMap("/a/cashgame/edit/2000-01-01").To<CashgameController>(HttpMethod.Post, x => x.Edit_Post("a", "2000-01-01", new CashgameEditPostModel()));
        }

        [Test]
        public void RunningCashgame()
        {
            _routes.ShouldMap("/a/cashgame/running").To<CashgameController>(x => x.Running("a"));
        }

        [Test]
        public void CashgameMatrix()
        {
            _routes.ShouldMap("/a/cashgame/matrix").To<CashgameController>(x => x.Matrix("a", null));
            _routes.ShouldMap("/a/cashgame/matrix/1").To<CashgameController>(x => x.Matrix("a", 1));
        }

        [Test]
        public void CashgameFacts()
        {
            _routes.ShouldMap("/a/cashgame/facts").To<CashgameController>(x => x.Facts("a", null));
            _routes.ShouldMap("/a/cashgame/facts/1").To<CashgameController>(x => x.Facts("a", 1));
        }

        [Test]
        public void CashgameToplist()
        {
            _routes.ShouldMap("/a/cashgame/toplist").To<CashgameController>(x => x.Toplist("a", null, null));
            _routes.ShouldMap("/a/cashgame/toplist/1").To<CashgameController>(x => x.Toplist("a", null, 1));
        }

        [Test]
        public void CashgameList()
        {
            _routes.ShouldMap("/a/cashgame/list").To<CashgameController>(x => x.List("a", null));
            _routes.ShouldMap("/a/cashgame/list/1").To<CashgameController>(x => x.List("a", 1));
        }

        [Test]
        public void CashgameChart()
        {
            _routes.ShouldMap("/a/cashgame/chart").To<CashgameController>(x => x.Chart("a", null));
            _routes.ShouldMap("/a/cashgame/chart/1").To<CashgameController>(x => x.Chart("a", 1));
        }

        [Test]
        public void CashgameChartJson()
        {
            _routes.ShouldMap("/a/cashgame/chartjson").To<CashgameController>(x => x.ChartJson("a", null));
            _routes.ShouldMap("/a/cashgame/chartjson/1").To<CashgameController>(x => x.ChartJson("a", 1));
        }

        [Test]
        public void CashgameBuyin()
        {
            _routes.ShouldMap("/a/cashgame/buyin/1").To<CashgameController>(x => x.Buyin("a", 1));
            _routes.ShouldMap("/a/cashgame/buyin/1").To<CashgameController>(HttpMethod.Post, x => x.Buyin_Post("a", 1, new BuyinPostModel()));
        }

        [Test]
        public void CashgameReport()
        {
            _routes.ShouldMap("/a/cashgame/report/1").To<CashgameController>(x => x.Report("a", 1));
            _routes.ShouldMap("/a/cashgame/report/1").To<CashgameController>(HttpMethod.Post, x => x.Report_Post("a", 1, new ReportPostModel()));
        }

        [Test]
        public void CashgameCashout()
        {
            _routes.ShouldMap("/a/cashgame/cashout/1").To<CashgameController>(x => x.Cashout("a", 1));
            _routes.ShouldMap("/a/cashgame/cashout/1").To<CashgameController>(HttpMethod.Post, x => x.Cashout_Post("a", 1, new CashoutPostModel()));
        }

        [Test]
        public void CashgameEnd()
        {
            _routes.ShouldMap("/a/cashgame/end").To<CashgameController>(x => x.End("a"));
            _routes.ShouldMap("/a/cashgame/end").To<CashgameController>(HttpMethod.Post, x => x.End_Post("a", new EndGamePostModel()));
        }

        [Test]
        public void CashgameDetails()
        {
            _routes.ShouldMap("/a/cashgame/details/2001-01-01").To<CashgameController>(x => x.Details("a", "2001-01-01"));
        }

        [Test]
        public void CashgameDetailsChartJson()
        {
            _routes.ShouldMap("/a/cashgame/detailschartjson/2001-01-01").To<CashgameController>(x => x.DetailsChartJson("a", "2001-01-01"));
        }

        [Test]
        public void CashgameAction()
        {
            _routes.ShouldMap("/a/cashgame/action/2001-01-01/1").To<CashgameController>(x => x.Action("a", "2001-01-01", 1));
        }

        [Test]
        public void CashgameActionChartJson()
        {
            _routes.ShouldMap("/a/cashgame/actionchartjson/2001-01-01/1").To<CashgameController>(x => x.ActionChartJson("a", "2001-01-01", 1));
        }

        [Test]
        public void CashgameDelete()
        {
            _routes.ShouldMap("/a/cashgame/delete/2001-01-01").To<CashgameController>(x => x.Delete("a", "2001-01-01"));
        }

        [Test]
        public void EditCheckpoint()
        {
            _routes.ShouldMap("/a/cashgame/editcheckpoint/2001-01-01/1/2").To<CashgameController>(x => x.EditCheckpoint("a", "2001-01-01", 1, 2));
            _routes.ShouldMap("/a/cashgame/editcheckpoint/2001-01-01/1/2").To<CashgameController>(HttpMethod.Post, x => x.EditCheckpoint_Post("a", "2001-01-01", 1, 2, new EditCheckpointPostModel()));
        }

        [Test]
        public void DeleteCheckpoint()
        {
            _routes.ShouldMap("/a/cashgame/deletecheckpoint/2001-01-01/1/2").To<CashgameController>(x => x.DeleteCheckpoint("a", "2001-01-01", 1, 2));
        }

        [Test]
        public void SendEmail()
        {
            _routes.ShouldMap("/-/admin/sendemail").To<AdminController>(x => x.SendEmail());
        }
    }
}