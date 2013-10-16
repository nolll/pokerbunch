using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.Tests.ModelTests.UrlModels{

	public class UrlModelTests : WebMockContainer {

		[Test]
        public void HomeUrl()
        {
            var sut = GetSut();
		    var result = sut.GetHomeUrl();

			Assert.AreEqual("/", result);
		}

		[Test]
        public void LoginUrl()
        {
            var sut = GetSut();
            var result = sut.GetLoginUrl();

            Assert.AreEqual("/-/auth/login", result);
        }

        [Test]
        public void LogoutUrl()
        {
            var sut = GetSut();
            var result = sut.GetLogoutUrl();

            Assert.AreEqual("/-/auth/logout", result);
        }

		[Test]
        public void CashgameAddUrl(){
			var homegame = GetHomegame();

            var sut = GetSut();
		    var result = sut.GetCashgameAddUrl(homegame);

			Assert.AreEqual("/abc/cashgame/add", result);
		}

		[Test]
        public void CashgameChartUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameChartUrl(homegame, year);

			Assert.AreEqual("/abc/cashgame/chart/2010", result);
		}

		[Test]
        public void CashgameChartUrl_WithoutYear(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetCashgameChartUrl(homegame, null);

			Assert.AreEqual("/abc/cashgame/chart", result);
		}

		[Test]
        public void CashgameDeleteUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

            var sut = GetSut();
            var result = sut.GetCashgameDeleteUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/delete/2010-01-01", result);
		}

		[Test]
        public void CashgameDetailsUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

            var sut = GetSut();
            var result = sut.GetCashgameDetailsUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/details/2010-01-01", result);
		}

		[Test]
        public void CashgameEditUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

            var sut = GetSut();
            var result = sut.GetCashgameEditUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/edit/2010-01-01", result);
		}

		[Test]
        public void CashgameIndexUrl(){
			var homegame = GetHomegame();

            var sut = GetSut();
			var result = sut.GetCashgameIndexUrl(homegame);

			Assert.AreEqual("/abc/cashgame/index", result);
		}

		[Test]
        public void CashgameLeaderboardUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameLeaderboardUrl(homegame, year);

			Assert.AreEqual("/abc/cashgame/leaderboard/2010", result);
		}

		[Test]
        public void CashgameLeaderboardUrl_WithoutYear(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetCashgameLeaderboardUrl(homegame, null);

			Assert.AreEqual("/abc/cashgame/leaderboard", result);
		}

		[Test]
        public void CashgameMatrixUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameMatrixUrl(homegame, year);

			Assert.AreEqual("/abc/cashgame/matrix/2010", result);
		}

		[Test]
        public void CashgameMatrixUrl_WithoutYear(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetCashgameMatrixUrl(homegame, null);

			Assert.AreEqual("/abc/cashgame/matrix", result);
		}

		[Test]
        public void CashgameListingUrl_WithYear(){
			var homegame = GetHomegame();
			const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameListingUrl(homegame, year);

			Assert.AreEqual("/abc/cashgame/listing/2010", result);
		}

		[Test]
        public void CashgameListingUrl_WithoutYear(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetCashgameListingUrl(homegame, null);

			Assert.AreEqual("/abc/cashgame/listing", result);
		}

		[Test]
        public void CashgameActionUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};
		    var player = new Player {DisplayName = "a"};

            var sut = GetSut();
            var result = sut.GetCashgameActionUrl(homegame, cashgame, player);

			Assert.AreEqual("/abc/cashgame/action/2010-01-01/a", result);
		}

		[Test]
        public void CashgameBuyinUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

            var sut = GetSut();
            var result = sut.GetCashgameBuyinUrl(homegame, player);

			Assert.AreEqual("/abc/cashgame/buyin/a", result);
		}

		[Test]
        public void CashgameReportUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

            var sut = GetSut();
            var result = sut.GetCashgameReportUrl(homegame, player);

			Assert.AreEqual("/abc/cashgame/report/a", result);
		}

		[Test]
        public void CashgameCashoutUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

            var sut = GetSut();
            var result = sut.GetCashgameCashoutUrl(homegame, player);

			Assert.AreEqual("/abc/cashgame/cashout/a", result);
		}

		[Test]
        public void CashgamePublishUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var cashgame = new Cashgame {StartTime = DateTime.Parse("2010-01-01")};

            var sut = GetSut();
            var result = sut.GetCashgamePublishUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/publish/2010-01-01", result);
		}

		[Test]
        public void CashgameUnpublishUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
            var cashgame = new Cashgame { StartTime = DateTime.Parse("2010-01-01") };

            var sut = GetSut();
            var result = sut.GetCashgameUnpublishUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/unpublish/2010-01-01", result);
		}

		[Test]
        public void ChangePasswordConfirmationUrl(){
            var sut = GetSut();
            var result = sut.GetChangePasswordConfirmationUrl();

            Assert.AreEqual("/-/user/changedpassword", result);
		}

		[Test]
        public void ChangePasswordFormUrl(){
            var sut = GetSut();
            var result = sut.GetChangePasswordUrl();

            Assert.AreEqual("/-/user/changepassword", result);
		}

		[Test]
        public void ForgotPasswordConfirmationUrl(){
            var sut = GetSut();
            var result = sut.GetForgotPasswordConfirmationUrl();

            Assert.AreEqual("/-/user/passwordsent", result);
		}

		[Test]
        public void ForgotPasswordFormUrl(){
            var sut = GetSut();
            var result = sut.GetForgotPasswordUrl();

            Assert.AreEqual("/-/user/forgotpassword", result);
		}

		[Test]
        public void HomegameAddUrl(){
            var sut = GetSut();
            var result = sut.GetHomegameAddUrl();

            Assert.AreEqual("/-/homegame/add", result);
		}

		[Test]
        public void HomegameAddConfirmationUrl(){
            var sut = GetSut();
            var result = sut.GetHomegameAddConfirmationUrl();

            Assert.AreEqual("/-/homegame/created", result);
		}

		[Test]
        public void HomegameDetailsUrl(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetHomegameDetailsUrl(homegame);

            Assert.AreEqual("/abc/homegame/details", result);
		}

		[Test]
        public void HomegameEditUrl(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetHomegameEditUrl(homegame);

			Assert.AreEqual("/abc/homegame/edit", result);
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

            var sut = GetSut();
            var result = sut.GetHomegameJoinConfirmationUrl(homegame);

			Assert.AreEqual("/abc/homegame/joined", result);
		}

		[Test]
        public void HomegameListingUrl(){
            var sut = GetSut();
            var result = sut.GetHomegameListingUrl();

			Assert.AreEqual("/-/homegame/listing", result);
		}

		[Test]
        public void PlayerAddUrl(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetPlayerAddUrl(homegame);

			Assert.AreEqual("/abc/player/add", result);
		}

		[Test]
        public void PlayerAddConfirmationUrl(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetPlayerAddConfirmationUrl(homegame);

			Assert.AreEqual("/abc/player/created", result);
		}

		[Test]
        public void PlayerDeleteUrl(){
			var homegame = GetHomegame();
			var player = new Player {DisplayName = "a"};

            var sut = GetSut();
            var result = sut.GetPlayerDeleteUrl(homegame, player);

			Assert.AreEqual("/abc/player/delete/a", result);
		}

		[Test]
        public void PlayerDetailsUrl(){
			var homegame = GetHomegame();
            var player = new Player { DisplayName = "a" };

            var sut = GetSut();
            var result = sut.GetPlayerDetailsUrl(homegame, player);

			Assert.AreEqual("/abc/player/details/a", result);
		}

		[Test]
        public void PlayerIndexUrl(){
			var homegame = GetHomegame();

            var sut = GetSut();
            var result = sut.GetPlayerIndexUrl(homegame);

			Assert.AreEqual("/abc/player/index", result);
		}

		[Test]
        public void PlayerInviteUrl(){
			var homegame = GetHomegame();
            var player = new Player { DisplayName = "a" };

            var sut = GetSut();
            var result = sut.GetPlayerInviteUrl(homegame, player);

			Assert.AreEqual("/abc/player/invite/a", result);
		}

		[Test]
        public void SharingSettingsUrl(){
            var sut = GetSut();
            var result = sut.GetSharingSettingsUrl();

			Assert.AreEqual("/-/sharing", result);
		}

		[Test]
        public void TwitterSettingsUrl(){
            var sut = GetSut();
            var result = sut.GetTwitterSettingsUrl();

			Assert.AreEqual("/-/sharing/twitter", result);
		}

		[Test]
        public void TwitterStartShareUrl(){
            var sut = GetSut();
            var result = sut.GetTwitterStartShareUrl();

			Assert.AreEqual("/-/sharing/twitterstart", result);
		}

		[Test]
        public void TwitterStopShareUrl(){
            var sut = GetSut();
            var result = sut.GetTwitterStopShareUrl();

			Assert.AreEqual("/-/sharing/twitterstop", result);
		}

		[Test]
        public void UserAddConfirmationUrl(){
            var sut = GetSut();
            var result = sut.GetUserAddConfirmationUrl();

			Assert.AreEqual("/-/user/created", result);
		}

		[Test]
        public void AddUserUrl()
		{
		    var sut = GetSut();
		    var result = sut.GetAddUserUrl();

			Assert.AreEqual("/-/user/add", result);
		}

		[Test]
        public void UserDetailsUrl(){
			var user = new User {UserName = "a"};

            var sut = GetSut();
            var result = sut.GetUserDetailsUrl(user);

			Assert.AreEqual("/-/user/details/a", result);
		}

		[Test]
        public void UserEditUrl(){
            var user = new User { UserName = "a" };

            var sut = GetSut();
            var result = sut.GetUserEditUrl(user);

			Assert.AreEqual("/-/user/edit/a", result);
		}

		[Test]
        public void UserListingUrl(){
            var sut = GetSut();
            var result = sut.GetUserListingUrl();

			Assert.AreEqual("/-/user/listing", result);
		}

	    [Test]
	    public void TwitterCallBackUrl()
	    {
	        const string siteUrl = "http://siteurl";
	        Mocks.SettingsMock.Setup(o => o.GetSiteUrl()).Returns(siteUrl);

	        var sut = GetSut();
	        var result = sut.GetTwitterCallbackUrl();

            Assert.AreEqual("http://siteurl/-/sharing/twittercallback", result);
	    }

        private UrlProvider GetSut()
        {
            return new UrlProvider(Mocks.SettingsMock.Object);
        }

		private Homegame GetHomegame(){
			return new Homegame {Slug = "abc"};
		}

	}

}