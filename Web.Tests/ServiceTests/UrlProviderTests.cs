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
        public void CashgameAddUrl()
		{
		    const string slug = "a";

            var sut = GetSut();
		    var result = sut.GetCashgameAddUrl(slug);

			Assert.AreEqual("/a/cashgame/add", result);
		}

		[Test]
        public void CashgameChartUrl_WithYear()
		{
		    const string slug = "a";
			const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameChartUrl(slug, year);

			Assert.AreEqual("/a/cashgame/chart/2010", result);
		}

		[Test]
        public void CashgameChartUrl_WithoutYear(){
            const string slug = "a";
			
            var sut = GetSut();
            var result = sut.GetCashgameChartUrl(slug, null);

			Assert.AreEqual("/a/cashgame/chart", result);
		}

		[Test]
        public void CashgameDeleteUrl()
		{
		    const string slug = "a";
		    const string dateStr = "b";

            var sut = GetSut();
            var result = sut.GetCashgameDeleteUrl(slug, dateStr);

			Assert.AreEqual("/a/cashgame/delete/b", result);
		}

		[Test]
        public void CashgameDetailsUrl(){
            const string slug = "a";
            const string dateStr = "b";

            var sut = GetSut();
            var result = sut.GetCashgameDetailsUrl(slug, dateStr);

			Assert.AreEqual("/a/cashgame/details/b", result);
		}

		[Test]
        public void CashgameEditUrlModel_ReturnsCorrectUrl(){
		    const string slug = "a";
            const string dateStr = "b";

            var sut = GetSut();
            var result = sut.GetCashgameEditUrl(slug, dateStr);

			Assert.AreEqual("/a/cashgame/edit/b", result);
		}

		[Test]
        public void CashgameIndexUrl()
		{
		    const string slug = "a";

            var sut = GetSut();
			var result = sut.GetCashgameIndexUrl(slug);

			Assert.AreEqual("/a/cashgame/index", result);
		}

		[Test]
        public void CashgameToplistUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameToplistUrl(slug, year);

			Assert.AreEqual("/a/cashgame/toplist/2010", result);
		}

		[Test]
        public void CashgameToplistUrl_WithoutYear()
        {
            const string slug = "a";
			
            var sut = GetSut();
            var result = sut.GetCashgameToplistUrl(slug, null);

			Assert.AreEqual("/a/cashgame/toplist", result);
		}

		[Test]
        public void CashgameMatrixUrl_WithYear(){
            const string slug = "a";
            const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameMatrixUrl(slug, year);

			Assert.AreEqual("/a/cashgame/matrix/2010", result);
		}

		[Test]
        public void CashgameMatrixUrl_WithoutYear(){
            const string slug = "a";
			
            var sut = GetSut();
            var result = sut.GetCashgameMatrixUrl(slug, null);

			Assert.AreEqual("/a/cashgame/matrix", result);
		}

		[Test]
        public void CashgameListUrl_WithYear(){
            const string slug = "a";
            const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameListUrl(slug, year);

			Assert.AreEqual("/a/cashgame/list/2010", result);
		}

		[Test]
        public void CashgameListUrl_WithoutYear(){
            const string slug = "a";
			
            var sut = GetSut();
            var result = sut.GetCashgameListUrl(slug, null);

			Assert.AreEqual("/a/cashgame/list", result);
		}

		[Test]
        public void CashgameActionUrlModel_ReturnsCorrectUrl()
		{
		    const string slug = "a";
		    const string dateStr = "b";
		    const string playerName = "c";

		    var sut = GetSut();
            var result = sut.GetCashgameActionUrl(slug, dateStr, playerName);

			Assert.AreEqual("/a/cashgame/action/b/c", result);
		}

		[Test]
        public void CashgameBuyinUrlModel_ReturnsCorrectUrl(){
            const string slug = "a";
		    const string playerName = "b";

            var sut = GetSut();
            var result = sut.GetCashgameBuyinUrl(slug, playerName);

			Assert.AreEqual("/a/cashgame/buyin/b", result);
		}

		[Test]
        public void CashgameReportUrlModel_ReturnsCorrectUrl(){
            const string slug = "a";
            const string playerName = "b";

            var sut = GetSut();
            var result = sut.GetCashgameReportUrl(slug, playerName);

			Assert.AreEqual("/a/cashgame/report/b", result);
		}

		[Test]
        public void CashgameCashoutUrlModel_ReturnsCorrectUrl(){
            const string slug = "a";
            const string playerName = "b";

            var sut = GetSut();
            var result = sut.GetCashgameCashoutUrl(slug, playerName);

			Assert.AreEqual("/a/cashgame/cashout/b", result);
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
        public void HomegameDetailsUrl()
		{
		    const string slug = "a";

            var sut = GetSut();
            var result = sut.GetHomegameDetailsUrl(slug);

            Assert.AreEqual("/a/homegame/details", result);
		}

		[Test]
        public void HomegameEditUrl()
        {
            const string slug = "a";

            var sut = GetSut();
            var result = sut.GetHomegameEditUrl(slug);

			Assert.AreEqual("/a/homegame/edit", result);
		}

		[Test]
        public void HomegameJoinUrl()
        {
            const string slug = "a";

		    var sut = GetSut();
		    var result = sut.GetJoinHomegameUrl(slug);

			Assert.AreEqual("/a/homegame/join", result);
		}

		[Test]
        public void HomegameJoinConfirmationUrl()
		{
		    const string slug = "a";

            var sut = GetSut();
            var result = sut.GetHomegameJoinConfirmationUrl(slug);

			Assert.AreEqual("/a/homegame/joined", result);
		}

		[Test]
        public void HomegameListUrl(){
            var sut = GetSut();
            var result = sut.GetHomegameListUrl();

			Assert.AreEqual("/-/homegame/list", result);
		}

		[Test]
        public void PlayerAddUrl()
        {
            const string slug = "a";

            var sut = GetSut();
            var result = sut.GetPlayerAddUrl(slug);

			Assert.AreEqual("/a/player/add", result);
		}

		[Test]
        public void PlayerAddConfirmationUrl()
        {
            const string slug = "a";

            var sut = GetSut();
            var result = sut.GetPlayerAddConfirmationUrl(slug);

			Assert.AreEqual("/a/player/created", result);
		}

		[Test]
        public void PlayerDeleteUrl(){
            const string slug = "a";
            const string playerName = "b";

            var sut = GetSut();
            var result = sut.GetPlayerDeleteUrl(slug, playerName);

			Assert.AreEqual("/a/player/delete/b", result);
		}

		[Test]
        public void PlayerDetailsUrl(){
            const string slug = "a";
            const string playerName = "b";

            var sut = GetSut();
            var result = sut.GetPlayerDetailsUrl(slug, playerName);

			Assert.AreEqual("/a/player/details/b", result);
		}

		[Test]
        public void PlayerIndexUrl(){
			const string slug = "a";

            var sut = GetSut();
            var result = sut.GetPlayerIndexUrl(slug);

			Assert.AreEqual("/a/player/index", result);
		}

		[Test]
        public void PlayerInviteUrl(){
            const string slug = "a";
            const string playerName = "b";

            var sut = GetSut();
            var result = sut.GetPlayerInviteUrl(slug, playerName);

			Assert.AreEqual("/a/player/invite/b", result);
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
            const string userName = "a";

            var sut = GetSut();
            var result = sut.GetUserDetailsUrl(userName);

			Assert.AreEqual("/-/user/details/a", result);
		}

		[Test]
        public void UserEditUrl(){
            const string userName = "a";

            var sut = GetSut();
            var result = sut.GetUserEditUrl(userName);

			Assert.AreEqual("/-/user/edit/a", result);
		}

		[Test]
        public void UserListUrl(){
            var sut = GetSut();
            var result = sut.GetUserListUrl();

			Assert.AreEqual("/-/user/list", result);
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
	        const string slug = "a";
	        const string dateStr = "b";
	        const string playerName = "c";

            var sut = GetSut();
            var result = sut.GetCashgameActionChartJsonUrl(slug, dateStr, playerName);

            Assert.AreEqual("/a/cashgame/actionchartjson/b/c", result);
	    }

        [Test]
	    public void GetCashgameChartJsonUrl()
	    {
            const string slug = "a";
            const int year = 2010;

	        var sut = GetSut();
            var result = sut.GetCashgameChartJsonUrl(slug, year);

            Assert.AreEqual("/a/cashgame/chartjson/2010", result);
	    }

        [Test]
	    public void GetCashgameCheckpointDeleteUrl()
        {
            const string slug = "a";
            const string dateStr = "b";
            const string playerName = "c";
            const int checkpointId = 1;

	        var sut = GetSut();
            var result = sut.GetCashgameCheckpointDeleteUrl(slug, dateStr, playerName, checkpointId);

            Assert.AreEqual("/a/cashgame/deletecheckpoint/b/c/1", result);
	    }

        [Test]
	    public void GetCashgameDetailsChartJsonUrl()
	    {
            const string slug = "a";
            const string dateStr = "b";
            
	        var sut = GetSut();
            var result = sut.GetCashgameDetailsChartJsonUrl(slug, dateStr);

            Assert.AreEqual("/a/cashgame/detailschartjson/b", result);
	    }

        [Test]
	    public void GetCashgameEndUrl()
	    {
            const string slug = "a";

	        var sut = GetSut();
            var result = sut.GetCashgameEndUrl(slug);

            Assert.AreEqual("/a/cashgame/end", result);
	    }

        [Test]
	    public void GetCashgameFactsUrl()
	    {
            const string slug = "a";
            const int year = 2010;

	        var sut = GetSut();
            var result = sut.GetCashgameFactsUrl(slug, year);

            Assert.AreEqual("/a/cashgame/facts/2010", result);
	    }

        [Test]
	    public void GetPlayerInviteConfirmationUrl()
        {
            const string slug = "a";
            const string playerName = "b";

            var sut = GetSut();
            var result = sut.GetPlayerInviteConfirmationUrl(slug, playerName);

            Assert.AreEqual("/a/player/invited/b", result);
	    }

        [Test]
        public void GetRunningCashgameUrl()
        {
            const string slug = "a";

	        var sut = GetSut();
            var result = sut.GetRunningCashgameUrl(slug);

            Assert.AreEqual("/a/cashgame/running", result);
	    }

        private UrlProvider GetSut()
        {
            return new UrlProvider(
                GetMock<ISettings>().Object);
        }

	}

}