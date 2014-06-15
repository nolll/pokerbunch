using Application.Services;
using Application.Urls;
using NUnit.Framework;
using Tests.Common;
using Web.Models.UrlModels;
using Web.Services;

namespace Tests.Web.ServiceTests
{
    public class UrlProviderTests : MockContainer
    {
        [Test]
        public void HomeUrl_Relative_UrlIsRelative()
        {
            var result = new HomeUrl();
            
            Assert.AreEqual("/", result.Relative);
        }

        [Test]
        public void Absolute_UrlContainsSchemeAndDomain()
        {
            var result = new HomeUrl();

            Assert.AreEqual("http://pokerbunch.lan/", result.Absolute());
        }

        [Test]
        public void LoginUrl()
        {
            var result = new LoginUrl();

            Assert.AreEqual("/-/auth/login", result.Relative);
        }

        [Test]
        public void LogoutUrl()
        {
            var result = new LogoutUrl();

            Assert.AreEqual("/-/auth/logout", result.Relative);
        }

        [Test]
        public void CashgameAddUrl()
        {
            const string slug = "a";

            var result = new AddCashgameUrl(slug);

            Assert.AreEqual("/a/cashgame/add", result.Relative);
        }

        [Test]
        public void CashgameChartUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new CashgameChartUrl(slug, year);

            Assert.AreEqual("/a/cashgame/chart/2010", result.Relative);
        }

        [Test]
        public void CashgameChartUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new CashgameChartUrl(slug, null);

            Assert.AreEqual("/a/cashgame/chart", result.Relative);
        }

        [Test]
        public void CashgameDeleteUrl()
        {
            const string slug = "a";
            const string dateStr = "b";

            var result = new DeleteCashgameUrl(slug, dateStr);

            Assert.AreEqual("/a/cashgame/delete/b", result.Relative);
        }

        [Test]
        public void CashgameDetailsUrl()
        {
            const string slug = "a";
            const string dateStr = "b";

            var result = new CashgameDetailsUrl(slug, dateStr);

            Assert.AreEqual("/a/cashgame/details/b", result.Relative);
        }

        [Test]
        public void CashgameEditUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const string dateStr = "b";

            var result = new EditCashgameUrl(slug, dateStr);

            Assert.AreEqual("/a/cashgame/edit/b", result.Relative);
        }

        [Test]
        public void CashgameIndexUrl()
        {
            const string slug = "a";

            var result = new CashgameIndexUrl(slug);

            Assert.AreEqual("/a/cashgame/index", result.Relative);
        }

        [Test]
        public void CashgameToplistUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new TopListUrl(slug, year);

            Assert.AreEqual("/a/cashgame/toplist/2010", result.Relative);
        }

        [Test]
        public void CashgameToplistUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new TopListUrl(slug, null);

            Assert.AreEqual("/a/cashgame/toplist", result.Relative);
        }

        [Test]
        public void CashgameMatrixUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new CashgameMatrixUrl(slug, year);

            Assert.AreEqual("/a/cashgame/matrix/2010", result.Relative);
        }

        [Test]
        public void CashgameMatrixUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new CashgameMatrixUrl(slug, null);

            Assert.AreEqual("/a/cashgame/matrix", result.Relative);
        }

        [Test]
        public void CashgameListUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new CashgameListUrl(slug, year);

            Assert.AreEqual("/a/cashgame/list/2010", result.Relative);
        }

        [Test]
        public void CashgameListUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new CashgameListUrl(slug, null);

            Assert.AreEqual("/a/cashgame/list", result.Relative);
        }

        [Test]
        public void CashgameActionUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const string dateStr = "b";
            const int playerId = 1;

            var result = new CashgameActionUrl(slug, dateStr, playerId);

            Assert.AreEqual("/a/cashgame/action/b/1", result.Relative);
        }

        [Test]
        public void CashgameBuyinUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new CashgameBuyinUrl(slug, playerId);

            Assert.AreEqual("/a/cashgame/buyin/1", result.Relative);
        }

        [Test]
        public void CashgameReportUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new CashgameReportUrl(slug, playerId);

            Assert.AreEqual("/a/cashgame/report/1", result.Relative);
        }

        [Test]
        public void CashgameCashoutUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new CashgameCashoutUrl(slug, playerId);

            Assert.AreEqual("/a/cashgame/cashout/1", result.Relative);
        }

        [Test]
        public void ChangePasswordConfirmationUrl()
        {
            var result = new ChangePasswordConfirmationUrl();

            Assert.AreEqual("/-/user/changedpassword", result.Relative);
        }

        [Test]
        public void ChangePasswordFormUrl()
        {
            var result = new ChangePasswordUrl();

            Assert.AreEqual("/-/user/changepassword", result.Relative);
        }

        [Test]
        public void ForgotPasswordConfirmationUrl()
        {
            var result = new ForgotPasswordConfirmationUrl();

            Assert.AreEqual("/-/user/passwordsent", result.Relative);
        }

        [Test]
        public void ForgotPasswordFormUrl()
        {
            var result = new ForgotPasswordUrl();

            Assert.AreEqual("/-/user/forgotpassword", result.Relative);
        }

        [Test]
        public void HomegameAddUrl()
        {
            var result = new AddHomegameUrl();

            Assert.AreEqual("/-/homegame/add", result.Relative);
        }

        [Test]
        public void HomegameAddConfirmationUrl()
        {
            var result = new AddHomegameConfirmationUrl();

            Assert.AreEqual("/-/homegame/created", result.Relative);
        }

        [Test]
        public void HomegameDetailsUrl()
        {
            const string slug = "a";

            var result = new HomegameDetailsUrl(slug);

            Assert.AreEqual("/a/homegame/details", result.Relative);
        }

        [Test]
        public void HomegameEditUrl()
        {
            const string slug = "a";

            var result = new EditHomegameUrl(slug);

            Assert.AreEqual("/a/homegame/edit", result.Relative);
        }

        [Test]
        public void HomegameJoinUrl()
        {
            const string slug = "a";

            var result = new JoinHomeGameUrl(slug);

            Assert.AreEqual("/a/homegame/join", result.Relative);
        }

        [Test]
        public void HomegameJoinConfirmationUrl()
        {
            const string slug = "a";

            var result = new JoinHomegameConfirmationUrl(slug);

            Assert.AreEqual("/a/homegame/joined", result.Relative);
        }

        [Test]
        public void HomegameListUrl()
        {
            var result = new HomegameListUrl();

            Assert.AreEqual("/-/homegame/list", result.Relative);
        }

        [Test]
        public void PlayerAddUrl()
        {
            const string slug = "a";

            var result = new AddPlayerUrl(slug);

            Assert.AreEqual("/a/player/add", result.Relative);
        }

        [Test]
        public void PlayerAddConfirmationUrl()
        {
            const string slug = "a";

            var result = new AddPlayerConfirmationUrl(slug);

            Assert.AreEqual("/a/player/created", result.Relative);
        }

        [Test]
        public void PlayerDeleteUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new DeletePlayerUrl(slug, playerId);

            Assert.AreEqual("/a/player/delete/1", result.Relative);
        }

        [Test]
        public void PlayerDetailsUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new PlayerDetailsUrl(slug, playerId);

            Assert.AreEqual("/a/player/details/1", result.Relative);
        }

        [Test]
        public void PlayerIndexUrl()
        {
            const string slug = "a";

            var result = new PlayerIndexUrl(slug);

            Assert.AreEqual("/a/player/index", result.Relative);
        }

        [Test]
        public void PlayerInviteUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new InvitePlayerUrl(slug, playerId);

            Assert.AreEqual("/a/player/invite/1", result.Relative);
        }

        [Test]
        public void SharingSettingsUrl()
        {
            var result = new SharingSettingsUrl();

            Assert.AreEqual("/-/sharing", result.Relative);
        }

        [Test]
        public void TwitterSettingsUrl()
        {
            var result = new TwitterSettingsUrl();

            Assert.AreEqual("/-/sharing/twitter", result.Relative);
        }

        [Test]
        public void TwitterStartShareUrl()
        {
            var result = new TwitterStartShareUrl();

            Assert.AreEqual("/-/sharing/twitterstart", result.Relative);
        }

        [Test]
        public void TwitterStopShareUrl()
        {
            var result = new TwitterStopShareUrl();

            Assert.AreEqual("/-/sharing/twitterstop", result.Relative);
        }

        [Test]
        public void UserAddConfirmationUrl()
        {
            var result = new AddUserConfirmationUrl();

            Assert.AreEqual("/-/user/created", result.Relative);
        }

        [Test]
        public void AddUserUrl()
        {
            var result = new AddUserUrl();

            Assert.AreEqual("/-/user/add", result.Relative);
        }

        [Test]
        public void UserDetailsUrl()
        {
            const string userName = "a";

            var result = new UserDetailsUrl(userName);

            Assert.AreEqual("/-/user/details/a", result.Relative);
        }

        [Test]
        public void UserEditUrl()
        {
            const string userName = "a";

            var result = new EditUserUrl(userName);

            Assert.AreEqual("/-/user/edit/a", result.Relative);
        }

        [Test]
        public void UserListUrl()
        {
            var result = new UserListUrl();

            Assert.AreEqual("/-/user/list", result.Relative);
        }

        [Test]
        public void TwitterCallBackUrl()
        {
            var result = new TwitterCallbackUrl();

            Assert.AreEqual("/-/sharing/twittercallback", result.Relative);
        }

        [Test]
        public void GetCashgameActionChartJsonUrl()
        {
            const string slug = "a";
            const string dateStr = "b";
            const int playerId = 1;

            var result = new CashgameActionChartJsonUrl(slug, dateStr, playerId);

            Assert.AreEqual("/a/cashgame/actionchartjson/b/1", result.Relative);
        }

        [Test]
        public void GetCashgameChartJsonUrl()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new CashgameChartJsonUrl(slug, year);

            Assert.AreEqual("/a/cashgame/chartjson/2010", result.Relative);
        }

        [Test]
        public void GetCashgameCheckpointDeleteUrl()
        {
            const string slug = "a";
            const string dateStr = "b";
            const int playerId = 1;
            const int checkpointId = 2;

            var result = new DeleteCheckpointUrl(slug, dateStr, playerId, checkpointId);

            Assert.AreEqual("/a/cashgame/deletecheckpoint/b/1/2", result.Relative);
        }

        [Test]
        public void GetCashgameCheckpointEditUrl()
        {
            const string slug = "a";
            const string dateStr = "b";
            const int playerId = 1;
            const int checkpointId = 2;

            var result = new EditCheckpointUrl(slug, dateStr, playerId, checkpointId);

            Assert.AreEqual("/a/cashgame/editcheckpoint/b/1/2", result.Relative);
        }

        [Test]
        public void GetCashgameDetailsChartJsonUrl()
        {
            const string slug = "a";
            const string dateStr = "b";

            var result = new CashgameDetailsChartJsonUrl(slug, dateStr);

            Assert.AreEqual("/a/cashgame/detailschartjson/b", result.Relative);
        }

        [Test]
        public void GetCashgameEndUrl()
        {
            const string slug = "a";

            var result = new EndCashgameUrl(slug);

            Assert.AreEqual("/a/cashgame/end", result.Relative);
        }

        [Test]
        public void GetCashgameFactsUrl()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new CashgameFactsUrl(slug, year);

            Assert.AreEqual("/a/cashgame/facts/2010", result.Relative);
        }

        [Test]
        public void GetPlayerInviteConfirmationUrl()
        {
            const string slug = "a";
            const int playerId = 1;

            var result = new InvitePlayerConfirmationUrl(slug, playerId);

            Assert.AreEqual("/a/player/invited/1", result.Relative);
        }

        [Test]
        public void GetRunningCashgameUrl()
        {
            const string slug = "a";

            var result = new RunningCashgameUrl(slug);

            Assert.AreEqual("/a/cashgame/running", result.Relative);
        }

        private UrlProvider GetSut()
        {
            return new UrlProvider(
                GetMock<ISettings>().Object);
        }
    }
}