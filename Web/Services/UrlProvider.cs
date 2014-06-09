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

        public string GetLoginUrl()
        {
            return new LoginUrlModel().Relative;
        }

        public string GetAddUserUrl()
        {
            return new AddUserUrlModel().Relative;
        }

        public string GetJoinHomegameUrl(string slug)
        {
            return new JoinHomeGameUrlModel(slug).Relative;
        }

        public string GetTwitterCallbackUrl()
        {
            return string.Format(RouteFormats.TwitterCallback, _settings.GetSiteUrl());
        }

        public string GetCashgameActionChartJsonUrl(string slug, string dateStr, int playerId)
        {
            return new CashgameActionChartJsonUrlModel(slug, dateStr, playerId).Relative;
        }

        public string GetCashgameAddUrl(string slug)
        {
            return new AddCashgameUrlModel(slug).Relative;
        }

        public string GetCashgameChartJsonUrl(string slug, int? year)
        {
            return new CashgameChartJsonUrlModel(slug, year).Relative;
        }

        public string GetCashgameChartUrl(string slug, int? year)
        {
            return new CashgameChartUrlModel(slug, year).Relative;
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
            return new DeleteCashgameUrlModel(slug, dateStr).Relative;
        }

        public string GetCashgameDetailsChartJsonUrl(string slug, string dateStr)
        {
            return new CashgameDetailsChartJsonUrl(slug, dateStr).Relative;
        }

        public string GetCashgameDetailsUrl(string slug, string dateStr)
        {
            return new CashgameDetailsUrl(slug, dateStr).Relative;
        }

        public string GetCashgameEditUrl(string slug, string dateStr)
        {
            return new EditCashgameUrl(slug, dateStr).Relative;
        }

        public string GetCashgameEndUrl(string slug)
        {
            return new EndCashgameUrlModel(slug).Relative;
        }

        public string GetCashgameToplistUrl(string slug, int? year)
        {
            return GetCashgameToplistUrlStatic(slug, year).Relative;
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
            return new SharingSettingsUrlModel().Relative;
        }

        public string GetTwitterSettingsUrl()
        {
            return new TwitterSettingsUrlModel().Relative;
        }

        public string GetTwitterStartShareUrl()
        {
            return new TwitterStartShareUrlModel().Relative;
        }

        public string GetTwitterStopShareUrl()
        {
            return new TwitterStopShareUrlModel().Relative;
        }
        
        public static string FormatCashgamePlayerUrl(string format, string slug, string dateStr, int playerId)
        {
            return string.Format(format, slug, dateStr, playerId);
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

    public class CashgameFactsUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameFactsUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, slug, year)
        {
        }
    }

    public class EndCashgameUrlModel : HomegameUrlModel
    {
        public EndCashgameUrlModel(string slug)
            : base(RouteFormats.CashgameEnd, slug)
        {
        }
    }

    public class CashgameChartUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameChartUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }

    public class CashgameChartJsonUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameChartJsonUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, slug, year)
        {
        }
    }

    public class AddCashgameUrlModel : HomegameUrlModel
    {
        public AddCashgameUrlModel(string slug)
            : base(RouteFormats.CashgameAdd, slug)
        {
        }
    }

    public class CashgameDetailsUrl : CashgameUrlModel
    {
        public CashgameDetailsUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameDetails, slug, dateStr)
        {
        }
    }

    public class EditCashgameUrl : CashgameUrlModel
    {
        public EditCashgameUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameEdit, slug, dateStr)
        {
        }
    }

    public class CashgameDetailsChartJsonUrl : CashgameUrlModel
    {
        public CashgameDetailsChartJsonUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameDetailsChartJson, slug, dateStr)
        {
        }
    }

    public class DeleteCashgameUrlModel : CashgameUrlModel
    {
        public DeleteCashgameUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameDelete, slug, dateStr)
        {
        }
    }

    public class CashgameActionUrlModel : CashgamePlayerUrlModel
    {
        public CashgameActionUrlModel(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameAction, slug, dateStr, playerId)
        {
        }
    }

    public class CashgameActionChartJsonUrlModel : CashgamePlayerUrlModel
    {
        public CashgameActionChartJsonUrlModel(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameActionChartJson, slug, dateStr, playerId)
        {
        }
    }

    public class CashgameCashoutUrlModel : PlayerUrlModel
    {
        public CashgameCashoutUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameCashout, slug, playerId)
        {
        }
    }

    public class CashgameBuyinUrlModel : PlayerUrlModel
    {
        public CashgameBuyinUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameBuyin, slug, playerId)
        {
        }
    }

    public class EditUserUrlModel : UserUrlModel
    {
        public EditUserUrlModel(string userName)
            : base(RouteFormats.UserEdit, userName)
        {
        }
    }

    public class TwitterStartShareUrlModel : UrlModel
    {
        public TwitterStartShareUrlModel()
            : base(RouteFormats.TwitterStartShare)
        {
        }
    }

    public class TwitterSettingsUrlModel : UrlModel
    {
        public TwitterSettingsUrlModel()
            : base(RouteFormats.TwitterSettings)
        {
        }
    }

    public class TwitterStopShareUrlModel : UrlModel
    {
        public TwitterStopShareUrlModel()
            : base(RouteFormats.TwitterStopShare)
        {
        }
    }

    public class JoinHomeGameUrlModel : HomegameUrlModel
    {
        public JoinHomeGameUrlModel(string slug)
            : base(RouteFormats.HomegameJoin, slug)
        {
        }
    }

    public class AddUserConfirmationUrlModel : UrlModel
    {
        public AddUserConfirmationUrlModel()
            : base(RouteFormats.UserAddConfirmation)
        {
        }
    }
}