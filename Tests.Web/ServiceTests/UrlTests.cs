using NUnit.Framework;
using Web.Urls;
using Web.Urls.SiteUrls;

namespace Tests.Web.ServiceTests
{
    public class UrlTests
    {
        [Test]
        public void HomeUrl_Relative_UrlIsRelativeAndIsEmptyIsFalse()
        {
            var result = new HomeUrl();
            
            Assert.AreEqual("/", result.Relative);
        }

        [Test]
        public void Absolute_UrlContainsSchemeAndDomain()
        {
            var result = new HomeUrl();

            Assert.AreEqual("https://pokerbunch.com/", result.GetAbsoluteInTest());
        }

        [Test]
        public void LoginUrl()
        {
            var result = new LoginUrl();

            Assert.AreEqual("/auth/login", result.Relative);
        }

        [Test]
        public void LogoutUrl()
        {
            var result = new LogoutUrl();

            Assert.AreEqual("/auth/logout", result.Relative);
        }

        [Test]
        public void CashgameAddUrl()
        {
            const string slug = "a";

            var result = new AddCashgameUrl(slug);

            Assert.AreEqual("/cashgame/add/a", result.Relative);
        }

        [Test]
        public void CashgameChartUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new ChartUrl(slug, year);

            Assert.AreEqual("/cashgame/chart/a/2010", result.Relative);
        }

        [Test]
        public void CashgameChartUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new ChartUrl(slug, null);

            Assert.AreEqual("/cashgame/chart/a", result.Relative);
        }

        [Test]
        public void CashgameDeleteUrl()
        {
            const string id = "1";

            var result = new DeleteCashgameUrl(id);

            Assert.AreEqual("/cashgame/delete/1", result.Relative);
        }

        [Test]
        public void CashgameDetailsUrl()
        {
            const string id = "1";

            var result = new CashgameDetailsUrl(id);

            Assert.AreEqual("/cashgame/details/1", result.Relative);
        }

        [Test]
        public void CashgameEditUrlModel_ReturnsCorrectUrl()
        {
            const string id = "1";

            var result = new EditCashgameUrl(id);

            Assert.AreEqual("/cashgame/edit/1", result.Relative);
        }

        [Test]
        public void CashgameIndexUrl()
        {
            const string slug = "a";

            var result = new CashgameIndexUrl(slug);

            Assert.AreEqual("/cashgame/index/a", result.Relative);
        }

        [Test]
        public void CashgameToplistUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new TopListUrl(slug, year);

            Assert.AreEqual("/cashgame/toplist/a/2010", result.Relative);
        }

        [Test]
        public void CashgameToplistUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new TopListUrl(slug, null);

            Assert.AreEqual("/cashgame/toplist/a", result.Relative);
        }

        [Test]
        public void CashgameMatrixUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new MatrixUrl(slug, year);

            Assert.AreEqual("/cashgame/matrix/a/2010", result.Relative);
        }

        [Test]
        public void CashgameMatrixUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new MatrixUrl(slug, null);

            Assert.AreEqual("/cashgame/matrix/a", result.Relative);
        }

        [Test]
        public void CashgameListUrl_WithYear()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new CashgameListUrl(slug, year);

            Assert.AreEqual("/cashgame/list/a/2010", result.Relative);
        }

        [Test]
        public void CashgameListUrl_WithoutYear()
        {
            const string slug = "a";

            var result = new CashgameListUrl(slug, null);

            Assert.AreEqual("/cashgame/list/a", result.Relative);
        }

        [Test]
        public void CashgameActionUrlModel_ReturnsCorrectUrl()
        {
            const string cashgameId = "1";
            const string playerId = "2";

            var result = new CashgameActionUrl(cashgameId, playerId);

            Assert.AreEqual("/cashgame/action/1/2", result.Relative);
        }

        [Test]
        public void CashgameBuyinUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";

            var result = new CashgameBuyinUrl(slug);

            Assert.AreEqual("/cashgame/buyin/a", result.Relative);
        }

        [Test]
        public void CashgameReportUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";

            var result = new CashgameReportUrl(slug);

            Assert.AreEqual("/cashgame/report/a", result.Relative);
        }

        [Test]
        public void CashgameCashoutUrlModel_ReturnsCorrectUrl()
        {
            const string slug = "a";

            var result = new CashgameCashoutUrl(slug);

            Assert.AreEqual("/cashgame/cashout/a", result.Relative);
        }

        [Test]
        public void ChangePasswordConfirmationUrl()
        {
            var result = new ChangePasswordConfirmationUrl();

            Assert.AreEqual("/user/changedpassword", result.Relative);
        }

        [Test]
        public void ChangePasswordFormUrl()
        {
            var result = new ChangePasswordUrl();

            Assert.AreEqual("/user/changepassword", result.Relative);
        }

        [Test]
        public void ForgotPasswordConfirmationUrl()
        {
            var result = new ForgotPasswordConfirmationUrl();

            Assert.AreEqual("/user/passwordsent", result.Relative);
        }

        [Test]
        public void ForgotPasswordFormUrl()
        {
            var result = new ForgotPasswordUrl();

            Assert.AreEqual("/user/forgotpassword", result.Relative);
        }

        [Test]
        public void BunchAddUrl()
        {
            var result = new AddBunchUrl();

            Assert.AreEqual("/bunch/add", result.Relative);
        }

        [Test]
        public void BunchAddConfirmationUrl()
        {
            var result = new AddBunchConfirmationUrl();

            Assert.AreEqual("/bunch/created", result.Relative);
        }

        [Test]
        public void BunchDetailsUrl()
        {
            const string slug = "a";

            var result = new BunchDetailsUrl(slug);

            Assert.AreEqual("/bunch/details/a", result.Relative);
        }

        [Test]
        public void BunchEditUrl()
        {
            const string slug = "a";

            var result = new EditBunchUrl(slug);

            Assert.AreEqual("/bunch/edit/a", result.Relative);
        }

        [Test]
        public void BunchJoinUrl()
        {
            const string slug = "a";

            var result = new JoinBunchUrl(slug);

            Assert.AreEqual("/bunch/join/a", result.Relative);
        }

        [Test]
        public void BunchJoinUrlWithCode()
        {
            const string slug = "a";
            const string code = "b";

            var result = new JoinBunchUrl(slug, code);

            Assert.AreEqual("/bunch/join/a/b", result.Relative);
        }

        [Test]
        public void BunchJoinConfirmationUrl()
        {
            const string slug = "a";

            var result = new JoinBunchConfirmationUrl(slug);

            Assert.AreEqual("/bunch/joined/a", result.Relative);
        }

        [Test]
        public void BunchListAllUrl()
        {
            var result = new BunchListAllUrl();

            Assert.AreEqual("/bunch/all", result.Relative);
        }

        [Test]
        public void PlayerAddUrl()
        {
            const string slug = "a";

            var result = new AddPlayerUrl(slug);

            Assert.AreEqual("/player/add/a", result.Relative);
        }

        [Test]
        public void PlayerAddConfirmationUrl()
        {
            const string slug = "a";

            var result = new AddPlayerConfirmationUrl(slug);

            Assert.AreEqual("/player/created/a", result.Relative);
        }

        [Test]
        public void PlayerDeleteUrl()
        {
            const string playerId = "1";

            var result = new DeletePlayerUrl(playerId);

            Assert.AreEqual("/player/delete/1", result.Relative);
        }

        [Test]
        public void PlayerDetailsUrl()
        {
            const string playerId = "1";

            var result = new PlayerDetailsUrl(playerId);

            Assert.AreEqual("/player/details/1", result.Relative);
        }

        [Test]
        public void PlayerIndexUrl()
        {
            const string slug = "a";

            var result = new PlayerIndexUrl(slug);

            Assert.AreEqual("/player/list/a", result.Relative);
        }

        [Test]
        public void PlayerInviteUrl()
        {
            const string playerId = "1";

            var result = new InvitePlayerUrl(playerId);

            Assert.AreEqual("/player/invite/1", result.Relative);
        }

        [Test]
        public void UserAddConfirmationUrl()
        {
            var result = new AddUserConfirmationUrl();

            Assert.AreEqual("/user/created", result.Relative);
        }

        [Test]
        public void AddUserUrl()
        {
            var result = new AddUserUrl();

            Assert.AreEqual("/user/add", result.Relative);
        }

        [Test]
        public void UserDetailsUrl()
        {
            const string userName = "a";

            var result = new UserDetailsUrl(userName);

            Assert.AreEqual("/user/details/a", result.Relative);
        }

        [Test]
        public void UserEditUrl()
        {
            const string userName = "a";

            var result = new EditUserUrl(userName);

            Assert.AreEqual("/user/edit/a", result.Relative);
        }

        [Test]
        public void UserListUrl()
        {
            var result = new UserListUrl();

            Assert.AreEqual("/user/list", result.Relative);
        }

        [Test]
        public void GetCashgameCheckpointDeleteUrl()
        {
            const string cashgameId = "1";
            const string checkpointId = "2";
            
            var result = new DeleteCheckpointUrl(cashgameId, checkpointId);

            Assert.AreEqual("/cashgame/deletecheckpoint/1/2", result.Relative);
        }

        [Test]
        public void GetCashgameCheckpointEditUrl()
        {
            const string cashgameId = "1";
            const string checkpointId = "2";

            var result = new EditCheckpointUrl(cashgameId, checkpointId);

            Assert.AreEqual("/cashgame/editcheckpoint/1/2", result.Relative);
        }

        [Test]
        public void GetCashgameEndUrl()
        {
            const string slug = "a";

            var result = new EndCashgameUrl(slug);

            Assert.AreEqual("/cashgame/end/a", result.Relative);
        }

        [Test]
        public void GetCashgameFactsUrl()
        {
            const string slug = "a";
            const int year = 2010;

            var result = new FactsUrl(slug, year);

            Assert.AreEqual("/cashgame/facts/a/2010", result.Relative);
        }

        [Test]
        public void GetPlayerInviteConfirmationUrl()
        {
            const string playerId = "1";

            var result = new InvitePlayerConfirmationUrl(playerId);

            Assert.AreEqual("/player/invited/1", result.Relative);
        }

        [Test]
        public void GetRunningCashgameUrl()
        {
            const string slug = "a";

            var result = new RunningCashgameUrl(slug);

            Assert.AreEqual("/cashgame/running/a", result.Relative);
        }
    }

    public static class UrlExtensionsInTest
    {
        public static string GetAbsoluteInTest(this Url url)
        {
            return AbsoluteUrl.Create(url, "pokerbunch.com", "api.pokerbunch.com");
        }
    }
}