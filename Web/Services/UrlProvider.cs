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
            return FormatHomegame(RouteFormats.HomegameJoin, homegame.Slug);
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
            return FormatHomegame(RouteFormats.CashgameAdd, homegame.Slug);
        }

        public string GetCashgameBuyinUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.CashgameBuyin, homegame.Slug, player.DisplayName);
        }

        public string GetCashgameCashoutUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.CashgameCashout, homegame.Slug, player.DisplayName);
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
            return FormatHomegame(RouteFormats.CashgameEnd, homegame.Slug);
        }

        public string GetCashgameFactsUrl(Homegame homegame, int? year)
        {
            return FormatHomegameWithOptionalYear(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, homegame, year);
        }

        public string GetCashgameIndexUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.CashgameIndex, homegame.Slug);
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
            return FormatPlayer(RouteFormats.CashgameReport, homegame.Slug, player.DisplayName);
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
            return FormatHomegame(RouteFormats.HomegameDetails, homegame.Slug);
        }

        public string GetHomegameEditUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.HomegameEdit, homegame.Slug);
        }

        public string GetHomegameJoinConfirmationUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.HomegameJoinConfirmation, homegame.Slug);
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

        public string GetPlayerAddConfirmationUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.PlayerAddConfirmation, homegame.Slug);
        }

        public string GetPlayerAddUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.PlayerAdd, homegame.Slug);
        }

        public string GetPlayerDeleteUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.PlayerDelete, homegame.Slug, player.DisplayName);
        }

        public string GetPlayerDetailsUrl(string slug, string playerName)
        {
            return FormatPlayer(RouteFormats.PlayerDetails, slug, playerName);
        }

        public string GetPlayerIndexUrl(string slug)
        {
            return FormatHomegame(RouteFormats.PlayerIndex, slug);
        }

        public string GetPlayerInviteConfirmationUrl(string slug, string playerName)
        {
            return FormatPlayer(RouteFormats.PlayerInviteConfirmation, slug, playerName);
        }

        public string GetPlayerInviteUrl(Homegame homegame, Player player)
        {
            return FormatPlayer(RouteFormats.PlayerInvite, homegame.Slug, player.DisplayName);
        }

        public string GetRunningCashgameUrl(Homegame homegame)
        {
            return FormatHomegame(RouteFormats.RunningCashgame, homegame.Slug);
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

        public string GetUserListUrl()
        {
            return RouteFormats.UserList;
        }

        private string GetCashgamePlayerUrl(string format, Homegame homegame, Cashgame cashgame, Player player)
        {
            var isoDate = cashgame.StartTime.HasValue ? _globalization.FormatIsoDate(cashgame.StartTime.Value) : string.Empty;
            var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
            return string.Format(format, homegame.Slug, isoDate, encodedPlayerName);
        }

        private string FormatHomegameWithOptionalYear(string format, string formatWithYear, Homegame homegame, int? year)
        {
            return year.HasValue ? FormatHomegameWithYear(formatWithYear, homegame, year.Value) : FormatHomegame(format, homegame.Slug);
        }

        private string FormatHomegame(string format, string slug)
        {
            return string.Format(format, slug);
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

        private string FormatPlayer(string format, string slug, string playerName)
        {
            var encodedPlayerName = HttpUtility.UrlPathEncode(playerName);
            return string.Format(format, slug, encodedPlayerName);
        }

        private string FormatUser(string format, User user)
        {
            return string.Format(format, user.UserName);
        }

    }
}