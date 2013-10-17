using System.Web;
using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Services;
using Web.Formatters;
using Web.Models.UrlModels;
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
            return new CashgameDeleteUrlModel(homegame, cashgame).Url;
        }

        public string GetCashgameDetailsChartJsonUrl(Homegame homegame, Cashgame cashgame)
        {
            return new CashgameDetailsChartJsonUrlModel(homegame, cashgame).Url;
        }

        public string GetCashgameDetailsUrl(Homegame homegame, Cashgame cashgame)
        {
            return new CashgameDetailsUrlModel(homegame, cashgame).Url;
        }

        public string GetCashgameEditUrl(Homegame homegame, Cashgame cashgame)
        {
            return new CashgameEditUrlModel(homegame, cashgame).Url;
        }

        public string GetCashgameEndUrl(Homegame homegame)
        {
            return new CashgameEndUrlModel(homegame).Url;
        }

        public string GetCashgameFactsUrl(Homegame homegame, int? year)
        {
            return new CashgameFactsUrlModel(homegame, year).Url;
        }

        public string GetCashgameIndexUrl(Homegame homegame)
        {
            return new CashgameIndexUrlModel(homegame).Url;
        }

        public string GetCashgameLeaderboardUrl(Homegame homegame, int? year)
        {
            return new CashgameLeaderboardUrlModel(homegame, year).Url;
        }

        public string GetCashgameListingUrl(Homegame homegame, int? year)
        {
            return new CashgameListingUrlModel(homegame, year).Url;
        }

        public string GetCashgameMatrixUrl(Homegame homegame, int? year)
        {
            return new CashgameMatrixUrlModel(homegame, year).Url;
        }

        public string GetCashgameReportUrl(Homegame homegame, Player player)
        {
            return new CashgameReportUrlModel(homegame, player).Url;
        }

        public string GetChangePasswordConfirmationUrl()
        {
            return new ChangePasswordConfirmationUrlModel().Url;
        }

        public string GetChangePasswordUrl()
        {
            return new ChangePasswordUrlModel().Url;
        }

        public string GetForgotPasswordConfirmationUrl()
        {
            return new ForgotPasswordConfirmationUrlModel().Url;
        }

        public string GetForgotPasswordUrl()
        {
            return new ForgotPasswordUrlModel().Url;
        }

        public string GetHomegameAddConfirmationUrl()
        {
            return new HomegameAddConfirmationUrlModel().Url;
        }

        public string GetHomegameAddUrl()
        {
            return new HomegameAddUrlModel().Url;
        }

        public string GetHomegameDetailsUrl(Homegame homegame)
        {
            return new HomegameDetailsUrlModel(homegame).Url;
        }

        public string GetHomegameEditUrl(Homegame homegame)
        {
            return new HomegameEditUrlModel(homegame).Url;
        }

        public string GetHomegameJoinConfirmationUrl(Homegame homegame)
        {
            return new HomegameJoinConfirmationUrlModel(homegame).Url;
        }

        public string GetHomegameListingUrl()
        {
            return new HomegameListingUrlModel().Url;
        }

        public string GetHomeUrl()
        {
            return new HomeUrlModel().Url;
        }

        public string GetPlayerAddConfirmationUrl(Homegame homegame)
        {
            return new PlayerAddConfirmationUrlModel(homegame).Url;
        }

        public string GetPlayerAddUrl(Homegame homegame)
        {
            return new PlayerAddUrlModel(homegame).Url;
        }

        public string GetPlayerDeleteUrl(Homegame homegame, Player player)
        {
            return new PlayerDeleteUrlModel(homegame, player).Url;
        }

        public string GetPlayerDetailsUrl(Homegame homegame, Player player)
        {
            return new PlayerDetailsUrlModel(homegame, player).Url;
        }

        public string GetPlayerIndexUrl(Homegame homegame)
        {
            return new PlayerIndexUrlModel(homegame).Url;
        }

        public string GetPlayerInviteConfirmationUrl(Homegame homegame, Player player)
        {
            return new PlayerInviteConfirmationUrlModel(homegame, player).Url;
        }

        public string GetPlayerInviteUrl(Homegame homegame, Player player)
        {
            return new PlayerInviteUrlModel(homegame, player).Url;
        }

        public string GetRunningCashgameUrl(Homegame homegame)
        {
            return new RunningCashgameUrlModel(homegame).Url;
        }

        public string GetSharingSettingsUrl()
        {
            return new SharingSettingsUrlModel().Url;
        }

        public string GetTwitterSettingsUrl()
        {
            return new TwitterSettingsUrlModel().Url;
        }

        public string GetTwitterStartShareUrl()
        {
            return new TwitterStartShareUrlModel().Url;
        }

        public string GetTwitterStopShareUrl()
        {
            return new TwitterStopShareUrlModel().Url;
        }
        
        public string GetUserAddConfirmationUrl()
        {
            return new UserAddConfirmationUrlModel().Url;
        }

        public string GetUserDetailsUrl(User user)
        {
            return new UserDetailsUrlModel(user).Url;
        }

        public string GetUserEditUrl(User user)
        {
            return new UserEditUrlModel(user).Url;
        }

        public string GetUserListingUrl()
        {
            return new UserListingUrlModel().Url;
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

        private string GetHomegameYearUrl(string format, string formatWithYear, Homegame homegame, int? year)
        {
            return year.HasValue ? UrlFormatter.FormatHomegameWithYear(formatWithYear, homegame, year.Value) : UrlFormatter.FormatHomegame(format, homegame);
        }
    }
}