namespace Application.Services
{
    public interface IUrlProvider
    {
        string GetLogoutUrl();
        string GetAddUserUrl();
        string GetLoginUrl();
        string GetTwitterCallbackUrl();
        string GetChangePasswordConfirmationUrl();
        string GetChangePasswordUrl();
        string GetForgotPasswordConfirmationUrl();
        string GetForgotPasswordUrl();
        string GetHomegameAddConfirmationUrl();
        string GetHomegameAddUrl();
        string GetHomegameListUrl();
        string GetHomeUrl();
        string GetSharingSettingsUrl();
        string GetTwitterSettingsUrl();
        string GetTwitterStartShareUrl();
        string GetTwitterStopShareUrl();
        string GetUserAddConfirmationUrl();
        string GetUserListUrl();

        string GetUserDetailsUrl(string userName);
        string GetUserEditUrl(string userName);

        string GetJoinHomegameUrl(string slug);
        string GetCashgameAddUrl(string slug);
        string GetCashgameEndUrl(string slug);
        string GetCashgameIndexUrl(string slug);
        string GetHomegameDetailsUrl(string slug);
        string GetHomegameEditUrl(string slug);
        string GetHomegameJoinConfirmationUrl(string slug);
        string GetPlayerAddUrl(string slug);
        string GetPlayerIndexUrl(string slug);
        string GetRunningCashgameUrl(string slug);
        string GetPlayerAddConfirmationUrl(string slug);

        string GetCashgameChartJsonUrl(string slug, int? year);
        string GetCashgameChartUrl(string slug, int? year);
        string GetCashgameFactsUrl(string slug, int? year);
        string GetCashgameToplistUrl(string slug, int? year);
        string GetCashgameListUrl(string slug, int? year);
        string GetCashgameMatrixUrl(string slug, int? year);

        string GetCashgameBuyinUrl(string slug, string playerName);
        string GetCashgameCashoutUrl(string slug, string playerName);
        string GetCashgameReportUrl(string slug, string playerName);
        string GetPlayerInviteConfirmationUrl(string slug, string playerName);
        string GetPlayerDeleteUrl(string slug, string playerName);
        string GetPlayerDetailsUrl(string slug, string playerName);
        string GetPlayerInviteUrl(string slug, string playerName);

        string GetCashgameDeleteUrl(string slug, string dateStr);
        string GetCashgameDetailsChartJsonUrl(string slug, string dateStr);
        string GetCashgameDetailsUrl(string slug, string dateStr);
        string GetCashgameEditUrl(string slug, string dateStr);

        string GetCashgameActionChartJsonUrl(string slug, string dateStr, string playerName);
        string GetCashgameActionUrl(string slug, string dateStr, string playerName);

        string GetCashgameCheckpointDeleteUrl(string slug, string dateStr, string playerName, int checkpointId);
        
    }
}
