using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Web.Models.UrlModels;
using Web.Services;

namespace Tests.Web.ServiceTests
{

    public class UrlProviderTests : MockContainer
    {

        [Test]
        public void HomeUrl()
        {
            var result = new HomeUrlModel();
            
            Assert.AreEqual("/", result.Relative);
        }

        [Test]
        public void LoginUrl()
        {
            var result = new LoginUrlModel();

            Assert.AreEqual("/-/auth/login", result.Relative);
        }

        [Test]
        public void LogoutUrl()
        {
            var result = new LogoutUrlModel();

            Assert.AreEqual("/-/auth/logout", result.Relative);
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
        public void CashgameChartUrl_WithoutYear()
        {
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
        public void CashgameDetailsUrl()
        {
            const string slug = "a";
            const string dateStr = "b";

            var sut = GetSut();
            var result = sut.GetCashgameDetailsUrl(slug, dateStr);

            Assert.AreEqual("/a/cashgame/details/b", result);
        }

        [Test]
        public void CashgameEditUrlModel_ReturnsCorrectUrl()
        {
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

            var result = new CashgameIndexUrlModel(slug);

            Assert.AreEqual("/a/cashgame/index", result.Relative);
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
        public void CashgameMatrixUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameMatrixUrl(slug, year);

            Assert.AreEqual("/a/cashgame/matrix/2010", result);
        }

        [Test]
        public void CashgameMatrixUrl_WithoutYear()
        {
            const string slug = "a";

            var sut = GetSut();
            var result = sut.GetCashgameMatrixUrl(slug, null);

            Assert.AreEqual("/a/cashgame/matrix", result);
        }

        [Test]
        public void CashgameListUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var sut = GetSut();
            var result = sut.GetCashgameListUrl(slug, year);

            Assert.AreEqual("/a/cashgame/list/2010", result);
        }

        [Test]
        public void CashgameListUrl_WithoutYear()
        {
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
            const int playerId = 1;

            var result = new CashgameActionUrlModel(slug, dateStr, playerId);

            Assert.AreEqual("/a/cashgame/action/b/1", result.Relative);
        }

        [Test]
        public void CashgameBuyinUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new CashgameBuyinUrlModel(slug, playerId);

            Assert.AreEqual("/a/cashgame/buyin/1", result.Relative);
        }

        [Test]
        public void CashgameReportUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var sut = GetSut();
            var result = sut.GetCashgameReportUrl(slug, playerId);

            Assert.AreEqual("/a/cashgame/report/1", result);
        }

        [Test]
        public void CashgameCashoutUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new CashgameCashoutUrlModel(slug, playerId);

            Assert.AreEqual("/a/cashgame/cashout/1", result.Relative);
        }

        [Test]
        public void ChangePasswordConfirmationUrl()
        {
            var sut = GetSut();
            var result = sut.GetChangePasswordConfirmationUrl();

            Assert.AreEqual("/-/user/changedpassword", result);
        }

        [Test]
        public void ChangePasswordFormUrl()
        {
            var sut = GetSut();
            var result = sut.GetChangePasswordUrl();

            Assert.AreEqual("/-/user/changepassword", result);
        }

        [Test]
        public void ForgotPasswordConfirmationUrl()
        {
            var sut = GetSut();
            var result = sut.GetForgotPasswordConfirmationUrl();

            Assert.AreEqual("/-/user/passwordsent", result);
        }

        [Test]
        public void ForgotPasswordFormUrl()
        {
            var sut = GetSut();
            var result = sut.GetForgotPasswordUrl();

            Assert.AreEqual("/-/user/forgotpassword", result);
        }

        [Test]
        public void HomegameAddUrl()
        {
            var sut = GetSut();
            var result = sut.GetHomegameAddUrl();

            Assert.AreEqual("/-/homegame/add", result);
        }

        [Test]
        public void HomegameAddConfirmationUrl()
        {
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
        public void HomegameListUrl()
        {
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
        public void PlayerDeleteUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var sut = GetSut();
            var result = sut.GetPlayerDeleteUrl(slug, playerId);

            Assert.AreEqual("/a/player/delete/1", result);
        }

        [Test]
        public void PlayerDetailsUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new PlayerDetailsUrlModel(slug, playerId);

            Assert.AreEqual("/a/player/details/1", result.Relative);
        }

        [Test]
        public void PlayerIndexUrl()
        {
            const string slug = "a";

            var result = new PlayerIndexUrlModel(slug);

            Assert.AreEqual("/a/player/index", result.Relative);
        }

        [Test]
        public void PlayerInviteUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var sut = GetSut();
            var result = sut.GetPlayerInviteUrl(slug, playerId);

            Assert.AreEqual("/a/player/invite/1", result);
        }

        [Test]
        public void SharingSettingsUrl()
        {
            var sut = GetSut();
            var result = sut.GetSharingSettingsUrl();

            Assert.AreEqual("/-/sharing", result);
        }

        [Test]
        public void TwitterSettingsUrl()
        {
            var sut = GetSut();
            var result = sut.GetTwitterSettingsUrl();

            Assert.AreEqual("/-/sharing/twitter", result);
        }

        [Test]
        public void TwitterStartShareUrl()
        {
            var sut = GetSut();
            var result = sut.GetTwitterStartShareUrl();

            Assert.AreEqual("/-/sharing/twitterstart", result);
        }

        [Test]
        public void TwitterStopShareUrl()
        {
            var sut = GetSut();
            var result = sut.GetTwitterStopShareUrl();

            Assert.AreEqual("/-/sharing/twitterstop", result);
        }

        [Test]
        public void UserAddConfirmationUrl()
        {
            var result = new AddUserConfirmationUrlModel();

            Assert.AreEqual("/-/user/created", result.Relative);
        }

        [Test]
        public void AddUserUrl()
        {
            var result = new AddUserUrlModel();

            Assert.AreEqual("/-/user/add", result.Relative);
        }

        [Test]
        public void UserDetailsUrl()
        {
            const string userName = "a";

            var result = new UserDetailsUrlModel(userName);

            Assert.AreEqual("/-/user/details/a", result.Relative);
        }

        [Test]
        public void UserEditUrl()
        {
            const string userName = "a";

            var result = new EditUserUrlModel(userName);

            Assert.AreEqual("/-/user/edit/a", result.Relative);
        }

        [Test]
        public void UserListUrl()
        {
            var result = new UserListUrlModel();

            Assert.AreEqual("/-/user/list", result.Relative);
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
            const int playerId = 1;

            var sut = GetSut();
            var result = sut.GetCashgameActionChartJsonUrl(slug, dateStr, playerId);

            Assert.AreEqual("/a/cashgame/actionchartjson/b/1", result);
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
            const int playerId = 1;
            const int checkpointId = 2;

            var sut = GetSut();
            var result = sut.GetCashgameCheckpointDeleteUrl(slug, dateStr, playerId, checkpointId);

            Assert.AreEqual("/a/cashgame/deletecheckpoint/b/1/2", result);
        }

        [Test]
        public void GetCashgameCheckpointEditUrl()
        {
            const string slug = "a";
            const string dateStr = "b";
            const int playerId = 1;
            const int checkpointId = 2;

            var sut = GetSut();
            var result = sut.GetCashgameCheckpointEditUrl(slug, dateStr, playerId, checkpointId);

            Assert.AreEqual("/a/cashgame/editcheckpoint/b/1/2", result);
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

            var result = new EndCashgameUrlModel(slug);

            Assert.AreEqual("/a/cashgame/end", result.Relative);
        }

        [Test]
        public void GetCashgameFactsUrl()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new CashgameFactsUrlModel(slug, year);

            Assert.AreEqual("/a/cashgame/facts/2010", result.Relative);
        }

        [Test]
        public void GetPlayerInviteConfirmationUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var sut = GetSut();
            var result = sut.GetPlayerInviteConfirmationUrl(slug, playerId);

            Assert.AreEqual("/a/player/invited/1", result);
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