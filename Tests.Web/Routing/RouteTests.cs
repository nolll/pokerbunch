﻿using System.Net.Http;
using System.Web.Routing;
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
using Web.Models.CashgameModels.Report;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;

namespace Tests.Web.Routing
{
    public class RouteTests
    {
        private readonly RouteCollection _routes;

        public RouteTests()
        {
            _routes = new RouteCollection();
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
            _routes.ShouldMap("/auth/login").To<LoginController>(x => x.Login(null));
            _routes.ShouldMap("/auth/login?returnUrl=a").To<LoginController>(x => x.Login("a"));
            _routes.ShouldMap("/auth/login").To<LoginController>(HttpMethod.Post, x => x.Post(new LoginPostModel()));
        }

        [Test]
        public void Logout()
        {
            _routes.ShouldMap("/auth/logout").To<LogoutController>(x => x.Logout());
        }

        [Test]
        public void UserDetailds()
        {
            _routes.ShouldMap("/user/details/a").To<UserDetailsController>(x => x.UserDetails("a"));
        }

        [Test]
        public void UserList()
        {
            _routes.ShouldMap("/user/list").To<UserListController>(x => x.List());
        }

        [Test]
        public void AddUser()
        {
            _routes.ShouldMap("/user/add").To<AddUserController>(x => x.AddUser());
            _routes.ShouldMap("/user/add").To<AddUserController>(HttpMethod.Post, x => x.Post(new AddUserPostModel()));
            _routes.ShouldMap("/user/created").To<AddUserController>(x => x.Done());
        }

        [Test]
        public void EditUser()
        {
            _routes.ShouldMap("/user/edit/a").To<EditUserController>(x => x.EditUser("a"));
            _routes.ShouldMap("/user/edit/a").To<EditUserController>(HttpMethod.Post, x => x.Post("a", new EditUserPostModel()));
        }

        [Test]
        public void ChangePassword()
        {
            _routes.ShouldMap("/user/changepassword").To<ChangePasswordController>(x => x.ChangePassword());
            _routes.ShouldMap("/user/changepassword").To<ChangePasswordController>(HttpMethod.Post, x => x.Post(new ChangePasswordPostModel()));
            _routes.ShouldMap("/user/changedpassword").To<ChangePasswordController>(x => x.Done());
        }

        [Test]
        public void ForgotPassword()
        {
            _routes.ShouldMap("/user/forgotpassword").To<ForgotPasswordController>(x => x.ForgotPassword());
            _routes.ShouldMap("/user/forgotpassword").To<ForgotPasswordController>(HttpMethod.Post, x => x.Post(new ForgotPasswordPostModel()));
            _routes.ShouldMap("/user/passwordsent").To<ForgotPasswordController>(x => x.Done());
        }

        [Test]
        public void BunchDetails()
        {
            _routes.ShouldMap("/bunch/details/a").To<BunchDetailsController>(x => x.Details("a"));
        }

        [Test]
        public void EditBunch()
        {
            _routes.ShouldMap("/bunch/edit/a").To<EditBunchController>(x => x.Edit("a"));
            _routes.ShouldMap("/bunch/edit/a").To<EditBunchController>(HttpMethod.Post, x => x.Edit_Post("a", new EditBunchPostModel()));
        }

        [Test]
        public void BunchList()
        {
            _routes.ShouldMap("/bunch/list").To<BunchListController>(x => x.List());
        }

        [Test]
        public void AddBunch()
        {
            _routes.ShouldMap("/bunch/add").To<AddBunchController>(x => x.Add());
            _routes.ShouldMap("/bunch/add").To<AddBunchController>(HttpMethod.Post, x => x.Add_Post(new AddBunchPostModel()));
            _routes.ShouldMap("/bunch/created").To<AddBunchController>(x => x.Created());
        }

        [Test]
        public void JoinBunch()
        {
            _routes.ShouldMap("/bunch/join/a").To<JoinBunchController>(x => x.Join("a"));
            _routes.ShouldMap("/bunch/join/a").To<JoinBunchController>(HttpMethod.Post, x => x.Post("a", new JoinBunchPostModel()));
            _routes.ShouldMap("/bunch/joined/a").To<JoinBunchController>(x => x.Joined("a"));
        }

        [Test]
        public void PlayerIndex()
        {
            _routes.ShouldMap("/player/index/a").To<PlayerIndexController>(x => x.Index("a"));
        }

        [Test]
        public void AddPlayerRoute()
        {
            _routes.ShouldMap("/player/add/a").To<AddPlayerController>(x => x.Add("a"));
            _routes.ShouldMap("/player/add/a").To<AddPlayerController>(HttpMethod.Post, x => x.Add_Post("a", new AddPlayerPostModel()));
            _routes.ShouldMap("/player/created/a").To<AddPlayerController>(x => x.Created("a"));
        }

        [Test]
        public void PlayerDetails()
        {
            _routes.ShouldMap("/player/details/1").To<PlayerDetailsController>(x => x.Details(1));
        }

        [Test]
        public void DeletePlayer()
        {
            _routes.ShouldMap("/player/delete/a/1").To<DeletePlayerController>(x => x.Delete("a", 1));
        }

        [Test]
        public void InvitePlayer()
        {
            _routes.ShouldMap("/player/invite/a/1").To<InvitePlayerController>(x => x.Invite("a", 1));
            _routes.ShouldMap("/player/invite/a/1").To<InvitePlayerController>(HttpMethod.Post, x => x.Invite_Post("a", 1, new InvitePlayerPostModel()));
            _routes.ShouldMap("/player/invited/a/1").To<InvitePlayerController>(x => x.Invited("a", 1));
        }

        [Test]
        public void CashgameIndex()
        {
            _routes.ShouldMap("/cashgame/index/a").To<CashgameIndexController>(x => x.Index("a"));
        }

        [Test]
        public void AddCashgame()
        {
            _routes.ShouldMap("/cashgame/add/a").To<AddCashgameController>(x => x.AddCashgame("a"));
            _routes.ShouldMap("/cashgame/add/a").To<AddCashgameController>(HttpMethod.Post, x => x.Post("a", new AddCashgamePostModel()));
        }

        [Test]
        public void EditCashgame()
        {
            _routes.ShouldMap("/cashgame/edit/a/2000-01-01").To<EditCashgameController>(x => x.Edit("a", "2000-01-01"));
            _routes.ShouldMap("/cashgame/edit/a/2000-01-01").To<EditCashgameController>(HttpMethod.Post, x => x.Post("a", "2000-01-01", new EditCashgamePostModel()));
        }

        [Test]
        public void RunningCashgame()
        {
            _routes.ShouldMap("/cashgame/running/a").To<RunningCashgameController>(x => x.Running("a"));
        }

        [Test]
        public void CashgameMatrix()
        {
            _routes.ShouldMap("/cashgame/matrix/a").To<MatrixController>(x => x.Matrix("a", null));
            _routes.ShouldMap("/cashgame/matrix/a/1").To<MatrixController>(x => x.Matrix("a", 1));
        }

        [Test]
        public void CashgameFacts()
        {
            _routes.ShouldMap("/cashgame/facts/a").To<CashgameFactsController>(x => x.Facts("a", null));
            _routes.ShouldMap("/cashgame/facts/a/1").To<CashgameFactsController>(x => x.Facts("a", 1));
        }

        [Test]
        public void CashgameToplist()
        {
            _routes.ShouldMap("/cashgame/toplist/a").To<TopListController>(x => x.Toplist("a", null, null));
            _routes.ShouldMap("/cashgame/toplist/a/1").To<TopListController>(x => x.Toplist("a", null, 1));
        }

        [Test]
        public void CashgameList()
        {
            _routes.ShouldMap("/cashgame/list/a").To<CashgameListController>(x => x.List("a", null, null));
            _routes.ShouldMap("/cashgame/list/a?orderby=b").To<CashgameListController>(x => x.List("a", null, "b"));
            _routes.ShouldMap("/cashgame/list/a/1").To<CashgameListController>(x => x.List("a", 1, null));
        }

        [Test]
        public void CashgameChart()
        {
            _routes.ShouldMap("/cashgame/chart/a").To<CashgameChartController>(x => x.Chart("a", null));
            _routes.ShouldMap("/cashgame/chart/a/1").To<CashgameChartController>(x => x.Chart("a", 1));
        }

        [Test]
        public void CashgameBuyin()
        {
            _routes.ShouldMap("/cashgame/buyin/a").To<CashgameBuyinController>(HttpMethod.Post, x => x.Buyin_Post("a", new BuyinPostModel()));
        }

        [Test]
        public void CashgameReport()
        {
            _routes.ShouldMap("/cashgame/report/a").To<CashgameReportController>(HttpMethod.Post, x => x.Report_Post("a", new ReportPostModel()));
        }

        [Test]
        public void CashgameCashout()
        {
            _routes.ShouldMap("/cashgame/cashout/a").To<CashgameCashoutController>(HttpMethod.Post, x => x.Cashout_Post("a", new CashoutPostModel()));
        }

        [Test]
        public void CashgameEnd()
        {
            _routes.ShouldMap("/cashgame/end/a").To<EndCashgameController>(HttpMethod.Post, x => x.Post("a"));
        }

        [Test]
        public void CashgameDetails()
        {
            _routes.ShouldMap("/cashgame/details/1").To<CashgameDetailsController>(x => x.Details(1));
        }

        [Test]
        public void CashgameAction()
        {
            _routes.ShouldMap("/cashgame/action/a/2001-01-01/1").To<CashgameActionController>(x => x.Action("a", "2001-01-01", 1));
        }

        [Test]
        public void CashgameDelete()
        {
            _routes.ShouldMap("/cashgame/delete/1").To<DeleteCashgameController>(x => x.Delete(1));
        }

        [Test]
        public void EditCheckpoint()
        {
            _routes.ShouldMap("/cashgame/editcheckpoint/a/2001-01-01/1/2").To<EditCheckpointController>(x => x.EditCheckpoint("a", "2001-01-01", 1, 2));
            _routes.ShouldMap("/cashgame/editcheckpoint/a/2001-01-01/1/2").To<EditCheckpointController>(HttpMethod.Post, x => x.EditCheckpoint_Post("a", "2001-01-01", 1, 2, new EditCheckpointPostModel()));
        }

        [Test]
        public void DeleteCheckpoint()
        {
            _routes.ShouldMap("/cashgame/deletecheckpoint/a/2001-01-01/1/2").To<DeleteCheckpointController>(x => x.DeleteCheckpoint("a", "2001-01-01", 1, 2));
        }

        [Test]
        public void Events()
        {
            _routes.ShouldMap("/event/list/a").To<EventListController>(x => x.List("a"));
            _routes.ShouldMap("/event/details/a/1").To<EventDetailsController>(x => x.List("a", 1));
        }

        [Test]
        public void Admin()
        {
            _routes.ShouldMap("/admin/clearcache").To<AdminController>(x => x.ClearCache());
            _routes.ShouldMap("/admin/sendemail").To<AdminController>(x => x.SendEmail());
        }

        [Test]
        public void Errors()
        {
            _routes.ShouldMap("/error/notfound").To<ErrorController>(x => x.NotFound());
            _routes.ShouldMap("/error/unauthorized").To<ErrorController>(x => x.Unauthorized());
            _routes.ShouldMap("/error/forbidden").To<ErrorController>(x => x.Forbidden());
            _routes.ShouldMap("/error/servererror").To<ErrorController>(x => x.ServerError());
        }
    }
}