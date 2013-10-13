using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.Tests.ModelTests.UrlModels{

	public class UrlModelTests : MockContainer {

		[Test]
        public void HomeUrl(){
			var sut = new HomeUrlModel();

			Assert.AreEqual("/", sut.Url);
		}

		[Test]
        public void AuthLoginUrl(){
			var sut = new AuthLoginUrlModel();

			Assert.AreEqual("/-/auth/login", sut.Url);
		}

		[Test]
        public void AuthLogoutUrl(){
			var sut = new AuthLogoutUrlModel();

			Assert.AreEqual("/-/auth/logout", sut.Url);
		}

		[Test]
        public void CashgameAddUrl(){
			var homegame = GetHomegame();

			var sut = new CashgameAddUrlModel(homegame);

			Assert.AreEqual("/abc/cashgame/add", sut.Url);
		}

		[Test]
        public void CashgameChartUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;

			var sut = new CashgameChartUrlModel(homegame, year);

			Assert.AreEqual("/abc/cashgame/chart/2010", sut.Url);
		}

		[Test]
        public void CashgameChartUrl_WithoutYear(){
			var homegame = GetHomegame();

			var sut = new CashgameChartUrlModel(homegame, null);

			Assert.AreEqual("/abc/cashgame/chart", sut.Url);
		}

		[Test]
        public void CashgameDeleteUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

			var sut = new CashgameDeleteUrlModel(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/delete/2010-01-01", sut.Url);
		}

		[Test]
        public void CashgameDetailsUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

			var sut = new CashgameDetailsUrlModel(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/details/2010-01-01", sut.Url);
		}

		[Test]
        public void CashgameEditUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

			var sut = new CashgameEditUrlModel(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/edit/2010-01-01", sut.Url);
		}

		[Test]
        public void CashgameIndexUrl(){
			var homegame = GetHomegame();

			var sut = new CashgameIndexUrlModel(homegame);

			Assert.AreEqual("/abc/cashgame/index", sut.Url);
		}

		[Test]
        public void CashgameLeaderboardUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;
            
            var sut = new CashgameLeaderboardUrlModel(homegame, year);

			Assert.AreEqual("/abc/cashgame/leaderboard/2010", sut.Url);
		}

		[Test]
        public void CashgameLeaderboardUrl_WithoutYear(){
			var homegame = GetHomegame();

			var sut = new CashgameLeaderboardUrlModel(homegame, null);

			Assert.AreEqual("/abc/cashgame/leaderboard", sut.Url);
		}

		[Test]
        public void CashgameMatrixUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;

			var sut = new CashgameMatrixUrlModel(homegame, year);

			Assert.AreEqual("/abc/cashgame/matrix/2010", sut.Url);
		}

		[Test]
        public void CashgameMatrixUrl_WithoutYear(){
			var homegame = GetHomegame();

			var sut = new CashgameMatrixUrlModel(homegame, null);

			Assert.AreEqual("/abc/cashgame/matrix", sut.Url);
		}

		[Test]
        public void CashgameListingUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;

			var sut = new CashgameListingUrlModel(homegame, year);

			Assert.AreEqual("/abc/cashgame/listing/2010", sut.Url);
		}

		[Test]
        public void CashgameListingUrl_WithoutYear(){
			var homegame = GetHomegame();

			var sut = new CashgameListingUrlModel(homegame, null);

			Assert.AreEqual("/abc/cashgame/listing", sut.Url);
		}

		[Test]
        public void CashgameActionUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};
		    var player = new Player {DisplayName = "a"};

		    var sut = new CashgameActionUrlModel(homegame, cashgame, player);

			Assert.AreEqual("/abc/cashgame/action/2010-01-01/a", sut.Url);
		}

		[Test]
        public void CashgameBuyinUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

		    var sut = new CashgameBuyinUrlModel(homegame, player);

			Assert.AreEqual("/abc/cashgame/buyin/a", sut.Url);
		}

		[Test]
        public void CashgameReportUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

			var sut = new CashgameReportUrlModel(homegame, player);

			Assert.AreEqual("/abc/cashgame/report/a", sut.Url);
		}

		[Test]
        public void CashgameCashoutUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

			var sut = new CashgameCashoutUrlModel(homegame, player);

			Assert.AreEqual("/abc/cashgame/cashout/a", sut.Url);
		}

		[Test]
        public void CashgamePublishUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

			var sut = new CashgamePublishUrlModel(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/publish/2010-01-01", sut.Url);
		}

		[Test]
        public void CashgameUnpublishUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
            var cashgame = new Cashgame { StartTime = DateTime.Parse("2010-01-01") };

			var sut = new CashgameUnpublishUrlModel(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/unpublish/2010-01-01", sut.Url);
		}

		[Test]
        public void ChangePasswordConfirmationUrl(){
			var sut = new ChangePasswordConfirmationUrlModel();

            Assert.AreEqual("/-/user/changedpassword", sut.Url);
		}

		[Test]
        public void ChangePasswordFormUrl(){
			var sut = new ChangePasswordUrlModel();

            Assert.AreEqual("/-/user/changepassword", sut.Url);
		}

		[Test]
        public void ForgotPasswordConfirmationUrl(){
			var sut = new ForgotPasswordConfirmationUrlModel();

            Assert.AreEqual("/-/user/passwordsent", sut.Url);
		}

		[Test]
        public void ForgotPasswordFormUrl(){
			var sut = new ForgotPasswordUrlModel();

            Assert.AreEqual("/-/user/forgotpassword", sut.Url);
		}

		[Test]
        public void HomegameAddUrl(){
			var sut = new HomegameAddUrlModel();

            Assert.AreEqual("/-/homegame/add", sut.Url);
		}

		[Test]
        public void HomegameAddConfirmationUrl(){
			var sut = new HomegameAddConfirmationUrlModel();

            Assert.AreEqual("/-/homegame/created", sut.Url);
		}

		[Test]
        public void HomegameDetailsUrl(){
			var homegame = GetHomegame();

			var sut = new HomegameDetailsUrlModel(homegame);

            Assert.AreEqual("/abc/homegame/details", sut.Url);
		}

		[Test]
        public void HomegameEditUrl(){
			var homegame = GetHomegame();

			var sut = new HomegameEditUrlModel(homegame);

			Assert.AreEqual("/abc/homegame/edit", sut.Url);
		}

		[Test]
        public void HomegameJoinUrl(){
			var homegame = GetHomegame();

		    var sut = GetSut();
		    var result = sut.GetJoinHomegameUrl(homegame);

			Assert.AreEqual("/abc/homegame/join", result);
		}

		[Test]
        public void HomegameJoinConfirmationUrl(){
			var homegame = GetHomegame();

			var sut = new HomegameJoinConfirmationUrlModel(homegame);

			Assert.AreEqual("/abc/homegame/joined", sut.Url);
		}

		[Test]
        public void HomegameListingUrl(){
			var sut = new HomegameListingUrlModel();

			Assert.AreEqual("/-/homegame/listing", sut.Url);
		}

		[Test]
        public void PlayerAddUrl(){
			var homegame = GetHomegame();

			var sut = new PlayerAddUrlModel(homegame);

			Assert.AreEqual("/abc/player/add", sut.Url);
		}

		[Test]
        public void PlayerAddConfirmationUrl(){
			var homegame = GetHomegame();

			var sut = new PlayerAddConfirmationUrlModel(homegame);

			Assert.AreEqual("/abc/player/created", sut.Url);
		}

		[Test]
        public void PlayerDeleteUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

		    var sut = new PlayerDeleteUrlModel(homegame, player);

			Assert.AreEqual("/abc/player/delete/a", sut.Url);
		}

		[Test]
        public void PlayerDetailsUrl(){
			var homegame = GetHomegame();
            var player = new Player { DisplayName = "a" };

			var sut = new PlayerDetailsUrlModel(homegame, player);

			Assert.AreEqual("/abc/player/details/a", sut.Url);
		}

		[Test]
        public void PlayerIndexUrl(){
			var homegame = GetHomegame();

			var sut = new PlayerIndexUrlModel(homegame);

			Assert.AreEqual("/abc/player/index", sut.Url);
		}

		[Test]
        public void PlayerInviteUrl(){
			var homegame = GetHomegame();
            var player = new Player { DisplayName = "a" };

			var sut = new PlayerInviteUrlModel(homegame, player);

			Assert.AreEqual("/abc/player/invite/a", sut.Url);
		}

		[Test]
        public void SharingSettingsUrl(){
			var sut = new SharingSettingsUrlModel();

			Assert.AreEqual("/-/sharing", sut.Url);
		}

		[Test]
        public void TwitterSettingsUrl(){
			var sut = new TwitterSettingsUrlModel();

			Assert.AreEqual("/-/sharing/twitter", sut.Url);
		}

		[Test]
        public void TwitterStartShareUrl(){
			var sut = new TwitterStartShareUrlModel();

			Assert.AreEqual("/-/sharing/twitterstart", sut.Url);
		}

		[Test]
        public void TwitterStopShareUrl(){
			var sut = new TwitterStopShareUrlModel();

			Assert.AreEqual("/-/sharing/twitterstop", sut.Url);
		}

		[Test]
        public void UserAddConfirmationUrl(){
			var sut = new UserAddConfirmationUrlModel();

			Assert.AreEqual("/-/user/created", sut.Url);
		}

		[Test]
        public void UserAddFormUrl(){
			var sut = new UserAddUrlModel();

			Assert.AreEqual("/-/user/add", sut.Url);
		}

		[Test]
        public void UserDetailsUrl(){
			var user = new User {UserName = "a"};

		    var sut = new UserDetailsUrlModel(user);

			Assert.AreEqual("/-/user/details/a", sut.Url);
		}

		[Test]
        public void UserEditUrl(){
            var user = new User { UserName = "a" };

			var sut = new UserEditUrlModel(user);

			Assert.AreEqual("/-/user/edit/a", sut.Url);
		}

		[Test]
        public void UserListingUrl(){
			var sut = new UserListingUrlModel();

			Assert.AreEqual("/-/user/listing", sut.Url);
		}

        private UrlProvider GetSut()
        {
            return new UrlProvider(WebMocks.SettingsMock.Object);
        }

		private Homegame GetHomegame(){
			return new Homegame {Slug = "abc"};
		}

	}

}