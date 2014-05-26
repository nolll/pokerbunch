using System.Web;
using Application.Services;
using Web.Routing;

namespace Web.Services
{
    public class UrlProvider : IUrlProvider
    {
        private readonly ISettings _settings;

        public UrlProvider(ISettings settings)
        {
            _settings = settings;
        }

        public string GetLoginUrl()
        {
            return RouteFormats.AuthLogin;
        }

        public string GetLogoutUrl()
        {
            return RouteFormats.AuthLogout;
        }

        public string GetAddUserUrl()
        {
            return RouteFormats.UserAdd;
        }

        public string GetJoinHomegameUrl(string slug)
        {
            return FormatHomegame(RouteFormats.HomegameJoin, slug);
        }

        public string GetTwitterCallbackUrl()
        {
            return string.Format(RouteFormats.TwitterCallback, _settings.GetSiteUrl());
        }

        public string GetCashgameActionChartJsonUrl(string slug, string dateStr, int playerId)
        {
            return GetCashgamePlayerUrl(RouteFormats.CashgameActionChartJson, slug, dateStr, playerId);
        }

        public string GetCashgameActionUrl(string slug, string dateStr, int playerId)
        {
            return GetCashgamePlayerUrl(RouteFormats.CashgameAction, slug, dateStr, playerId);
        }

        public string GetCashgameAddUrl(string slug)
        {
            return FormatHomegame(RouteFormats.CashgameAdd, slug);
        }

        public string GetCashgameBuyinUrl(string slug, int playerId)
        {
            return FormatPlayer(RouteFormats.CashgameBuyin, slug, playerId);
        }

        public string GetCashgameCashoutUrl(string slug, int playerId)
        {
            return FormatPlayer(RouteFormats.CashgameCashout, slug, playerId);
        }

        public string GetCashgameChartJsonUrl(string slug, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, slug, year);
        }

        public string GetCashgameChartUrl(string slug, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year);
        }

        public string GetCashgameCheckpointDeleteUrl(string slug, string dateStr, int playerId, int checkpointId)
        {
            return string.Format(RouteFormats.CashgameCheckpointDelete, slug, dateStr, playerId, checkpointId);
        }

        public string GetCashgameCheckpointEditUrl(string slug, string dateStr, int playerId, int checkpointId)
        {
            return string.Format(RouteFormats.CashgameCheckpointEdit, slug, dateStr, playerId, checkpointId);
        }

        public string GetCashgameDeleteUrl(string slug, string dateStr)
        {
            return FormatCashgame(RouteFormats.CashgameDelete, slug, dateStr);
        }

        public string GetCashgameDetailsChartJsonUrl(string slug, string dateStr)
        {
            return FormatCashgame(RouteFormats.CashgameDetailsChartJson, slug, dateStr);
        }

        public string GetCashgameDetailsUrl(string slug, string dateStr)
        {
            return FormatCashgame(RouteFormats.CashgameDetails, slug, dateStr);
        }

        public string GetCashgameEditUrl(string slug, string dateStr)
        {
            return FormatCashgame(RouteFormats.CashgameEdit, slug, dateStr);
        }

        public string GetCashgameEndUrl(string slug)
        {
            return FormatHomegame(RouteFormats.CashgameEnd, slug);
        }

        public string GetCashgameFactsUrl(string slug, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year);
        }

        public string GetCashgameIndexUrl(string slug)
        {
            return FormatHomegame(RouteFormats.CashgameIndex, slug);
        }

        public string GetCashgameToplistUrl(string slug, int? year)
        {
            return GetCashgameToplistUrlStatic(slug, year).ToString();
        }

        public static UrlModel GetCashgameToplistUrlStatic(string slug, int? year)
        {
            return new TopListUrlModel(slug, year);
        }

        public string GetCashgameListUrl(string slug, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year);
        }

        public string GetCashgameMatrixUrl(string slug, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year);
        }

        public string GetCashgameReportUrl(string slug, int playerId)
        {
            return FormatPlayer(RouteFormats.CashgameReport, slug, playerId);
        }

        public string GetChangePasswordConfirmationUrl()
        {
            return RouteFormats.ChangePasswordConfirmation;
        }

        public string GetChangePasswordUrl()
        {
            return RouteFormats.ChangePassword;
        }

        public string GetForgotPasswordConfirmationUrl()
        {
            return RouteFormats.ForgotPasswordConfirmation;
        }

        public string GetForgotPasswordUrl()
        {
            return RouteFormats.ForgotPassword;
        }

        public string GetHomegameAddConfirmationUrl()
        {
            return RouteFormats.HomegameAddConfirmation;
        }

        public string GetHomegameAddUrl()
        {
            return RouteFormats.HomegameAdd;
        }

        public string GetHomegameDetailsUrl(string slug)
        {
            return FormatHomegame(RouteFormats.HomegameDetails, slug);
        }

        public string GetHomegameEditUrl(string slug)
        {
            return FormatHomegame(RouteFormats.HomegameEdit, slug);
        }

        public string GetHomegameJoinConfirmationUrl(string slug)
        {
            return FormatHomegame(RouteFormats.HomegameJoinConfirmation, slug);
        }

        public string GetHomegameListUrl()
        {
            return RouteFormats.HomegameList;
        }

        public string GetHomeUrl()
        {
            return RouteFormats.Home;
        }

        public string GetPlayerAddConfirmationUrl(string slug)
        {
            return FormatHomegame(RouteFormats.PlayerAddConfirmation, slug);
        }

        public string GetPlayerAddUrl(string slug)
        {
            return FormatHomegame(RouteFormats.PlayerAdd, slug);
        }

        public string GetPlayerDeleteUrl(string slug, int playerId)
        {
            return FormatPlayer(RouteFormats.PlayerDelete, slug, playerId);
        }

        public string GetPlayerDetailsUrl(string slug, int playerId)
        {
            return GetPlayerDetailsUrlStatic(slug, playerId).ToString();
        }

        public static UrlModel GetPlayerDetailsUrlStatic(string slug, int playerId)
        {
            return new PlayerDetailsUrlModel(slug, playerId);
        }

        public string GetPlayerIndexUrl(string slug)
        {
            return FormatHomegame(RouteFormats.PlayerIndex, slug);
        }

        public string GetPlayerInviteConfirmationUrl(string slug, int playerId)
        {
            return FormatPlayer(RouteFormats.PlayerInviteConfirmation, slug, playerId);
        }

        public string GetPlayerInviteUrl(string slug, int playerId)
        {
            return FormatPlayer(RouteFormats.PlayerInvite, slug, playerId);
        }

        public string GetRunningCashgameUrl(string slug)
        {
            return FormatHomegame(RouteFormats.RunningCashgame, slug);
        }

        public string GetSharingSettingsUrl()
        {
            return RouteFormats.SharingSettings;
        }

        public string GetTwitterSettingsUrl()
        {
            return RouteFormats.TwitterSettings;
        }

        public string GetTwitterStartShareUrl()
        {
            return RouteFormats.TwitterStartShare;
        }

        public string GetTwitterStopShareUrl()
        {
            return RouteFormats.TwitterStopShare;
        }
        
        public string GetUserAddConfirmationUrl()
        {
            return RouteFormats.UserAddConfirmation;
        }

        public string GetUserDetailsUrl(string userName)
        {
            return FormatUser(RouteFormats.UserDetails, userName);
        }

        public string GetUserEditUrl(string userName)
        {
            return FormatUser(RouteFormats.UserEdit, userName);
        }

        public string GetUserListUrl()
        {
            return RouteFormats.UserList;
        }

        private string GetCashgamePlayerUrl(string format, string slug, string dateStr, int playerId)
        {
            return string.Format(format, slug, dateStr, playerId);
        }

        public static string FormatHomegameWithOptionalYear(string format, string formatWithYear, string slug, int? year)
        {
            return year.HasValue ? FormatHomegameWithYear(formatWithYear, slug, year.Value) : FormatHomegame(format, slug);
        }

        private static string FormatHomegame(string format, string slug)
        {
            return string.Format(format, slug);
        }

        private static string FormatHomegameWithYear(string format, string slug, int year)
        {
            return string.Format(format, slug, year);
        }

        private string FormatCashgame(string format, string slug, string dateStr)
        {
            return string.Format(format, slug, dateStr);
        }

        public static string FormatPlayer(string format, string slug, int playerId)
        {
            return string.Format(format, slug, playerId);
        }

        private string FormatUser(string format, string userName)
        {
            return string.Format(format, userName);
        }
    }

    public abstract class UrlModel
    {
        protected string Url;

        public override string ToString()
        {
            if (Url == null)
                return string.Empty;
            return Url;
        }
    }

    public class PlayerDetailsUrlModel : UrlModel
    {
        public PlayerDetailsUrlModel(string slug, int playerId)
        {
            Url = UrlProvider.FormatPlayer(RouteFormats.PlayerDetails, slug, playerId);
        }
    }

    public class TopListUrlModel : UrlModel
    {
        public TopListUrlModel(string slug, int? year)
        {
            Url = UrlProvider.FormatHomegameWithOptionalYear(RouteFormats.CashgameToplist, RouteFormats.CashgameToplistWithYear, slug, year);
        }
    }
}