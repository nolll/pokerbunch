using System.Web;
using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Services;
using Web.Formatters;
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

        public string GetJoinHomegameUrl(Homegame homegame)
        {
            return UrlFormatter.FormatHomegame(RouteFormats.HomegameJoin, homegame);
        }

        public string GetTwitterCallbackUrl()
        {
            return string.Format(RouteFormats.TwitterCallback, _settings.GetSiteUrl());
        }

        public string GetCashgameActionChartJsonUrl(Homegame homegame, Cashgame cashgame, Player player)
        {
            return GetCashgamePlayerUrl(RouteFormats.CashgameActionChartJson, homegame, cashgame, player);
        }

        public string GetCashgameActionUrl(Homegame homegame, Cashgame cashgame, Player player)
        {
            return GetCashgamePlayerUrl(RouteFormats.CashgameAction, homegame, cashgame, player);
        }

        public string GetCashgameAddUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.CashgameAdd, homegame);
        }

        public string GetCashgameBuyinUrl(Homegame homegame, Player player)
        {
            return GetPlayerUrl(RouteFormats.CashgameBuyin, homegame, player);
        }

        public string GetCashgameCashoutUrl(Homegame homegame, Player player)
        {
            return GetPlayerUrl(RouteFormats.CashgameCashout, homegame, player);
        }

        public string GetCashgameChartJsonUrl(Homegame homegame, int? year)
        {
            return GetHomegameYearUrl(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, homegame, year);
        }

        public string GetCashgameChartUrl(Homegame homegame, int? year)
        {
            return GetHomegameYearUrl(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, homegame, year);
        }

        public string GetCashgameCheckpointDeleteUrl(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            var isoDate = cashgame.StartTime.HasValue ? UrlFormatter.FormatIsoDate(cashgame.StartTime.Value) : string.Empty;
            var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
            return string.Format(RouteFormats.CashgameCheckpointDelete, homegame.Slug, isoDate, encodedPlayerName, checkpoint.Id);
        }

        public string GetCashgameDeleteUrl(Homegame homegame, Cashgame cashgame)
        {
            return GetCashgameUrl(RouteFormats.CashgameDelete, homegame, cashgame);
        }

        public string GetCashgameDetailsChartJsonUrl(Homegame homegame, Cashgame cashgame)
        {
            return GetCashgameUrl(RouteFormats.CashgameDetailsChartJson, homegame, cashgame);
        }

        public string GetCashgameDetailsUrl(Homegame homegame, Cashgame cashgame)
        {
            return GetCashgameUrl(RouteFormats.CashgameDetails, homegame, cashgame);
        }

        public string GetCashgameEditUrl(Homegame homegame, Cashgame cashgame)
        {
            return GetCashgameUrl(RouteFormats.CashgameEdit, homegame, cashgame);
        }

        public string GetCashgameEndUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.CashgameEnd, homegame);
        }

        public string GetCashgameFactsUrl(Homegame homegame, int? year)
        {
            return GetHomegameYearUrl(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, homegame, year);
        }

        public string GetCashgameIndexUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.CashgameIndex, homegame);
        }

        public string GetCashgameLeaderboardUrl(Homegame homegame, int? year)
        {
            return GetHomegameYearUrl(RouteFormats.CashgameLeaderboard, RouteFormats.CashgameLeaderboardWithYear, homegame, year);
        }

        public string GetCashgameListingUrl(Homegame homegame, int? year)
        {
            return GetHomegameYearUrl(RouteFormats.CashgameListing, RouteFormats.CashgameListingWithYear, homegame, year);
        }

        public string GetCashgameMatrixUrl(Homegame homegame, int? year)
        {
            return GetHomegameYearUrl(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, homegame, year);
        }

        public string GetCashgameReportUrl(Homegame homegame, Player player)
        {
            return GetPlayerUrl(RouteFormats.CashgameReport, homegame, player);
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

        public string GetHomegameDetailsUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.HomegameDetails, homegame);
        }

        public string GetHomegameEditUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.HomegameEdit, homegame);
        }

        public string GetHomegameJoinConfirmationUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.HomegameJoinConfirmation, homegame);
        }

        public string GetHomegameListingUrl()
        {
            return RouteFormats.HomegameListing;
        }

        public string GetHomeUrl()
        {
            return RouteFormats.Home;
        }

        public string GetPlayerAddConfirmationUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.PlayerAddConfirmation, homegame);
        }

        public string GetPlayerAddUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.PlayerAdd, homegame);
        }

        public string GetPlayerDeleteUrl(Homegame homegame, Player player)
        {
            return GetPlayerUrl(RouteFormats.PlayerDelete, homegame, player);
        }

        public string GetPlayerDetailsUrl(Homegame homegame, Player player)
        {
            return GetPlayerUrl(RouteFormats.PlayerDetails, homegame, player);
        }

        public string GetPlayerIndexUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.PlayerIndex, homegame);
        }

        public string GetPlayerInviteConfirmationUrl(Homegame homegame, Player player)
        {
            return GetPlayerUrl(RouteFormats.PlayerInviteConfirmation, homegame, player);
        }

        public string GetPlayerInviteUrl(Homegame homegame, Player player)
        {
            return GetPlayerUrl(RouteFormats.PlayerInvite, homegame, player);
        }

        public string GetRunningCashgameUrl(Homegame homegame)
        {
            return GetHomegameUrl(RouteFormats.RunningCashgame, homegame);
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

        public string GetUserDetailsUrl(User user)
        {
            return GetUserUrl(RouteFormats.UserDetails, user);
        }

        public string GetUserEditUrl(User user)
        {
            return GetUserUrl(RouteFormats.UserEdit, user);
        }

        public string GetUserListingUrl()
        {
            return RouteFormats.UserListing;
        }

        private string GetCashgamePlayerUrl(string format, Homegame homegame, Cashgame cashgame, Player player)
        {
            var isoDate = cashgame.StartTime.HasValue ? UrlFormatter.FormatIsoDate(cashgame.StartTime.Value) : string.Empty;
            var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
            return string.Format(format, homegame.Slug, isoDate, encodedPlayerName);
        }

        private string GetHomegameUrl(string format, Homegame homegame)
        {
            return UrlFormatter.FormatHomegame(format, homegame);
        }

        private string GetPlayerUrl(string format, Homegame homegame, Player player)
        {
            return UrlFormatter.FormatPlayer(format, homegame, player);
        }

        private string GetCashgameUrl(string format, Homegame homegame, Cashgame cashgame)
        {
            return UrlFormatter.FormatCashgame(format, homegame, cashgame);
        }

        private string GetUserUrl(string format, User user)
        {
            return UrlFormatter.FormatUser(format, user);
        }

        private string GetHomegameYearUrl(string format, string formatWithYear, Homegame homegame, int? year)
        {
            return year.HasValue ? UrlFormatter.FormatHomegameWithYear(formatWithYear, homegame, year.Value) : UrlFormatter.FormatHomegame(format, homegame);
        }
    }
}