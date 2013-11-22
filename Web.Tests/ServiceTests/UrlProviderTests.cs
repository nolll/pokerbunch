using System;
using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Services;
using Infrastructure.System;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Services;

namespace Web.Tests.ServiceTests{

	public class UrlProviderTests : MockContainer {

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
        public void CashgameDeleteUrl()
		{
		    const string formattedDate = "a";

			var homegame = GetHomegame();
		    var dateTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: dateTime);

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(dateTime)).Returns(formattedDate);

            var sut = GetSut();
            var result = sut.GetCashgameDeleteUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/delete/a", result);
		}

		[Test]
        public void CashgameDetailsUrl(){
            const string formattedDate = "a";
            
            var homegame = GetHomegame();
            var dateTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: dateTime);

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(dateTime)).Returns(formattedDate);

            var sut = GetSut();
            var result = sut.GetCashgameDetailsUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/details/a", result);
		}

		[Test]
        public void CashgameEditUrlModel_ReturnsCorrectUrl(){
            const string formattedDate = "a";
            
            var homegame = GetHomegame();
            var dateTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: dateTime);

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(dateTime)).Returns(formattedDate);

            var sut = GetSut();
            var result = sut.GetCashgameEditUrl(homegame, cashgame);

			Assert.AreEqual("/abc/cashgame/edit/a", result);
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
        public void CashgameActionUrlModel_ReturnsCorrectUrl()
		{
		    const string formattedDate = "a";
		    const string displayName = "b";

			var homegame = GetHomegame();
		    var dateTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: dateTime);
		    var player = new FakePlayer(displayName: displayName);

		    GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(dateTime)).Returns(formattedDate);

            var sut = GetSut();
            var result = sut.GetCashgameActionUrl(homegame, cashgame, player);

			Assert.AreEqual("/abc/cashgame/action/a/b", result);
		}

		[Test]
        public void CashgameBuyinUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new FakePlayer(displayName: "a");

            var sut = GetSut();
            var result = sut.GetCashgameBuyinUrl(homegame, player);

			Assert.AreEqual("/abc/cashgame/buyin/a", result);
		}

		[Test]
        public void CashgameReportUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new FakePlayer(displayName: "a");

            var sut = GetSut();
            var result = sut.GetCashgameReportUrl(homegame, player);

			Assert.AreEqual("/abc/cashgame/report/a", result);
		}

		[Test]
        public void CashgameCashoutUrlModel_ReturnsCorrectUrl(){
			var homegame = GetHomegame();
			var player = new FakePlayer(displayName: "a");

            var sut = GetSut();
            var result = sut.GetCashgameCashoutUrl(homegame, player);

			Assert.AreEqual("/abc/cashgame/cashout/a", result);
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
			var player = new FakePlayer(displayName: "a");

            var sut = GetSut();
            var result = sut.GetPlayerDeleteUrl(homegame, player);

			Assert.AreEqual("/abc/player/delete/a", result);
		}

		[Test]
        public void PlayerDetailsUrl(){
			var homegame = GetHomegame();
            var player = new FakePlayer(displayName: "a");

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
            var player = new FakePlayer(displayName: "a");

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
            var user = new FakeUser(userName: "a");

            var sut = GetSut();
            var result = sut.GetUserDetailsUrl(user);

			Assert.AreEqual("/-/user/details/a", result);
		}

		[Test]
        public void UserEditUrl(){
            var user = new FakeUser(userName: "a");

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
	        GetMock<ISettings>().Setup(o => o.GetSiteUrl()).Returns(siteUrl);

	        var sut = GetSut();
	        var result = sut.GetTwitterCallbackUrl();

            Assert.AreEqual("http://siteurl/-/sharing/twittercallback", result);
	    }

	    [Test]
	    public void GetCashgameActionChartJsonUrl()
	    {
	        const string formattedDate = "a";

            var homegame = GetHomegame();
	        var dateTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: dateTime);
            var player = new FakePlayer(displayName: "b");

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(dateTime)).Returns(formattedDate);

            var sut = GetSut();
            var result = sut.GetCashgameActionChartJsonUrl(homegame, cashgame, player);

            Assert.AreEqual("/abc/cashgame/actionchartjson/a/b", result);
	    }

        [Test]
	    public void GetCashgameChartJsonUrl()
	    {
            var homegame = GetHomegame();
            const int year = 2010;

	        var sut = GetSut();
            var result = sut.GetCashgameChartJsonUrl(homegame, year);

            Assert.AreEqual("/abc/cashgame/chartjson/2010", result);
	    }

        [Test]
	    public void GetCashgameCheckpointDeleteUrl()
        {
            const string formattedDate = "a";

            var homegame = GetHomegame();
            var dateTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: dateTime);
            var player = new FakePlayer(displayName: "b");
            var checkpoint = new FakeCheckpoint(id: 1);

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(dateTime)).Returns(formattedDate);

	        var sut = GetSut();
            var result = sut.GetCashgameCheckpointDeleteUrl(homegame, cashgame, player, checkpoint);

            Assert.AreEqual("/abc/cashgame/deletecheckpoint/a/b/1", result);
	    }

        [Test]
	    public void GetCashgameDetailsChartJsonUrl()
	    {
            const string formattedDate = "a";

            var homegame = GetHomegame();
            var dateTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: dateTime);

            GetMock<IGlobalization>().Setup(o => o.FormatIsoDate(dateTime)).Returns(formattedDate);
            
	        var sut = GetSut();
            var result = sut.GetCashgameDetailsChartJsonUrl(homegame, cashgame);

            Assert.AreEqual("/abc/cashgame/detailschartjson/a", result);
	    }

        [Test]
	    public void GetCashgameEndUrl()
	    {
            var homegame = GetHomegame();

	        var sut = GetSut();
            var result = sut.GetCashgameEndUrl(homegame);

            Assert.AreEqual("/abc/cashgame/end", result);
	    }

        [Test]
	    public void GetCashgameFactsUrl()
	    {
            var homegame = GetHomegame();
            const int year = 2010;

	        var sut = GetSut();
            var result = sut.GetCashgameFactsUrl(homegame, year);

            Assert.AreEqual("/abc/cashgame/facts/2010", result);
	    }

        [Test]
	    public void GetPlayerInviteConfirmationUrl()
	    {
            var homegame = GetHomegame();
            var player = new FakePlayer(displayName: "a");

            var sut = GetSut();
            var result = sut.GetPlayerInviteConfirmationUrl(homegame, player);

            Assert.AreEqual("/abc/player/invited/a", result);
	    }

        [Test]
        public void GetRunningCashgameUrl()
	    {
            var homegame = GetHomegame();

	        var sut = GetSut();
            var result = sut.GetRunningCashgameUrl(homegame);

            Assert.AreEqual("/abc/cashgame/running", result);
	    }

        private UrlProvider GetSut()
        {
            return new UrlProvider(
                GetMock<ISettings>().Object,
                GetMock<IGlobalization>().Object);
        }

		private Homegame GetHomegame(){
            return new FakeHomegame(slug: "abc");
		}

	}

}