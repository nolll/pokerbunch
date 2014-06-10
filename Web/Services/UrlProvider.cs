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

        public string GetHomegameDetailsUrl(string slug)
        {
            return new HomegameDetailsUrlModel(slug).Relative;
        }

        public string GetHomegameEditUrl(string slug)
        {
            return new EditHomegameUrlModel(slug).Relative;
        }

        public string GetHomegameJoinConfirmationUrl(string slug)
        {
            return new JoinHomegameConfirmationUrlModel(slug).Relative;
        }

        public string GetHomegameListUrl()
        {
            return new HomegameListUrlModel().Relative;
        }

        public string GetPlayerAddConfirmationUrl(string slug)
        {
            return new AddPlayerConfirmationUrlModel(slug).Relative;
        }

        public string GetPlayerAddUrl(string slug)
        {
            return new AddPlayerUrlModel(slug).Relative;
        }

        public string GetPlayerDeleteUrl(string slug, int playerId)
        {
            return new DeletePlayerUrlModel(slug, playerId).Relative;
        }

        public string GetPlayerInviteConfirmationUrl(string slug, int playerId)
        {
            return new InvitePlayerConfirmationUrlModel(slug, playerId).Relative;
        }

        public string GetPlayerInviteUrl(string slug, int playerId)
        {
            return new InvitePlayerUrlModel(slug, playerId).Relative;
        }

        public string GetRunningCashgameUrl(string slug)
        {
            return new RunningCashgameUrlModel(slug).Relative;
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

    public class EditCheckpointUrlModel : CheckpointUrlModel
    {
        public EditCheckpointUrlModel(string slug, string dateStr, int playerId, int checkpointId)
            : base(RouteFormats.CashgameCheckpointEdit, slug, dateStr, playerId, checkpointId)
        {
        }
    }
}