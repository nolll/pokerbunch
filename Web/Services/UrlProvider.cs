using System.Web;
using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Services;
using Infrastructure.System;
using Web.Routing;

namespace Web.Services
{
    public class UrlProvider : IUrlProvider
    {
        private readonly ISettings _settings;
        private readonly IGlobalization _globalization;

        public UrlProvider(
            ISettings settings,
            IGlobalization globalization)
        {
            _settings = settings;
            _globalization = globalization;
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
            return FormatHomegame(RouteFormats.HomegameJoin, homegame);
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
            return FormatHomegame(RouteFormats.CashgameAdd, homegame);
        }

        public string GetCashgameBuyinUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.CashgameBuyin, homegame, player);
        }

        public string GetCashgameCashoutUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.CashgameCashout, homegame, player);
        }

        public string GetCashgameChartJsonUrl(Homegame homegame, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, homegame, year);
        }

        public string GetCashgameChartUrl(Homegame homegame, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, homegame, year);
        }

        public string GetCashgameCheckpointDeleteUrl(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            var isoDate = cashgame.StartTime.HasValue ? _globalization.FormatIsoDate(cashgame.StartTime.Value) : string.Empty;
            var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
            return string.Format(RouteFormats.CashgameCheckpointDelete, homegame.Slug, isoDate, encodedPlayerName, checkpoint.Id);
        }

        public string GetCashgameDeleteUrl(Homegame homegame, Cashgame cashgame)
        {
            return FormatCashgame(RouteFormats.CashgameDelete, homegame, cashgame);
        }

        public string GetCashgameDetailsChartJsonUrl(Homegame homegame, Cashgame cashgame)
        {
            return FormatCashgame(RouteFormats.CashgameDetailsChartJson, homegame, cashgame);
        }

        public string GetCashgameDetailsUrl(Homegame homegame, Cashgame cashgame)
        {
            return FormatCashgame(RouteFormats.CashgameDetails, homegame, cashgame);
        }

        public string GetCashgameEditUrl(Homegame homegame, Cashgame cashgame)
        {
            return FormatCashgame(RouteFormats.CashgameEdit, homegame, cashgame);
        }

        public string GetCashgameEndUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.CashgameEnd, homegame);
        }

        public string GetCashgameFactsUrl(Homegame homegame, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, homegame, year);
        }

        public string GetCashgameIndexUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.CashgameIndex, homegame);
        }

        public string GetCashgameToplistUrl(Homegame homegame, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameToplist, RouteFormats.CashgameToplistWithYear, homegame, year);
        }

        public string GetCashgameListUrl(Homegame homegame, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, homegame, year);
        }

        public string GetCashgameMatrixUrl(Homegame homegame, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, homegame, year);
        }

        public string GetCashgameReportUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.CashgameReport, homegame, player);
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
            return FormatHomegame(RouteFormats.HomegameDetails, homegame);
        }

        public string GetHomegameEditUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.HomegameEdit, homegame);
        }

        public string GetHomegameJoinConfirmationUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.HomegameJoinConfirmation, homegame);
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
            return FormatHomegame(RouteFormats.PlayerAddConfirmation, homegame);
        }

        public string GetPlayerAddUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.PlayerAdd, homegame);
        }

        public string GetPlayerDeleteUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.PlayerDelete, homegame, player);
        }

        public string GetPlayerDetailsUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.PlayerDetails, homegame, player);
        }

        public string GetPlayerIndexUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.PlayerIndex, homegame);
        }

        public string GetPlayerInviteConfirmationUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.PlayerInviteConfirmation, homegame, player);
        }

        public string GetPlayerInviteUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.PlayerInvite, homegame, player);
        }

        public string GetRunningCashgameUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.RunningCashgame, homegame);
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
            return FormatUser(RouteFormats.UserDetails, user);
        }

        public string GetUserEditUrl(User user)
        {
            return FormatUser(RouteFormats.UserEdit, user);
        }

        public string GetUserListingUrl()
        {
            return RouteFormats.UserListing;
        }

        private string GetCashgamePlayerUrl(string format, Homegame homegame, Cashgame cashgame, Player player)
        {
            var isoDate = cashgame.StartTime.HasValue ? _globalization.FormatIsoDate(cashgame.StartTime.Value) : string.Empty;
            var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
            return string.Format(format, homegame.Slug, isoDate, encodedPlayerName);
        }

        private string FormatHomegameWithOptionalYear(string format, string formatWithYear, Homegame homegame, int? year)
        {
            return year.HasValue ? FormatHomegameWithYear(formatWithYear, homegame, year.Value) : FormatHomegame(format, homegame);
        }

        private string FormatHomegame(string format, Homegame homegame)
        {
            return string.Format(format, homegame.Slug);
        }

        private string FormatHomegameWithYear(string format, Homegame homegame, int year)
        {
            return string.Format(format, homegame.Slug, year);
        }

        private string FormatCashgame(string format, Homegame homegame, Cashgame cashgame)
        {
            if (cashgame.StartTime.HasValue)
            {
                var isoDate = _globalization.FormatIsoDate(cashgame.StartTime.Value);
                return string.Format(format, homegame.Slug, isoDate);
            }
            return null;
        }

        private string FormatPlayer(string format, Homegame homegame, Player player)
        {
            var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
            return string.Format(format, homegame.Slug, encodedPlayerName);
        }

        private string FormatUser(string format, User user)
        {
            return string.Format(format, user.UserName);
        }

    }
}