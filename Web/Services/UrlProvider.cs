using Application.Services;
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

        /* leave for now */
        public string GetLoginUrl()
        {
            return new LoginUrlModel().Relative;
        }

        /* leave for now */
        public string GetAddUserUrl()
        {
            return new AddUserUrlModel().Relative;
        }

        /* leave for now */
        public string GetJoinHomegameUrl(string slug)
        {
            return new JoinHomeGameUrlModel(slug).Relative;
        }

        /* leave for now */
        public string GetTwitterCallbackUrl()
        {
            return string.Format(RouteFormats.TwitterCallback, _settings.GetSiteUrl());
        }

        public static string FormatCashgamePlayerUrl(string format, string slug, string dateStr, int playerId)
        {
            return string.Format(format, slug, dateStr, playerId);
        }

        public static string FormatCheckpoint(string format, string slug, string dateStr, int playerId, int checkpointId)
        {
            return string.Format(format, slug, dateStr, playerId, checkpointId);
        }

        public static string FormatHomegameWithOptionalYear(string format, string formatWithYear, string slug, int? year)
        {
            return year.HasValue ? FormatHomegameWithYear(formatWithYear, slug, year.Value) : FormatHomegame(format, slug);
        }

        public static string FormatHomegame(string format, string slug)
        {
            return string.Format(format, slug);
        }

        private static string FormatHomegameWithYear(string format, string slug, int year)
        {
            return string.Format(format, slug, year);
        }

        public static string FormatCashgame(string format, string slug, string dateStr)
        {
            return string.Format(format, slug, dateStr);
        }

        public static string FormatPlayer(string format, string slug, int playerId)
        {
            return string.Format(format, slug, playerId);
        }

        public static string FormatUser(string format, string userName)
        {
            return string.Format(format, userName);
        }
    }
}