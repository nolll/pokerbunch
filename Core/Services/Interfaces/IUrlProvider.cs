using Core.Classes;
using Core.Classes.Checkpoints;

namespace Core.Services
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
        string GetUserEditUrl(User user);
        
        string GetJoinHomegameUrl(Homegame homegame);
        string GetCashgameAddUrl(Homegame homegame);
        string GetCashgameEndUrl(Homegame homegame);
        string GetCashgameIndexUrl(string slug);
        string GetHomegameDetailsUrl(Homegame homegame);
        string GetHomegameEditUrl(Homegame homegame);
        string GetHomegameJoinConfirmationUrl(Homegame homegame);
        string GetPlayerAddUrl(Homegame homegame);
        string GetPlayerIndexUrl(string slug);
        string GetRunningCashgameUrl(string slug);
        string GetPlayerAddConfirmationUrl(string slug);
        
        string GetCashgameChartJsonUrl(Homegame homegame, int? year);
        string GetCashgameChartUrl(Homegame homegame, int? year);
        string GetCashgameFactsUrl(Homegame homegame, int? year);
        string GetCashgameToplistUrl(Homegame homegame, int? year);
        string GetCashgameListUrl(Homegame homegame, int? year);
        string GetCashgameMatrixUrl(Homegame homegame, int? year);

        string GetCashgameBuyinUrl(Homegame homegame, Player player);
        string GetCashgameCashoutUrl(Homegame homegame, Player player);
        string GetCashgameReportUrl(Homegame homegame, Player player);
        string GetPlayerInviteConfirmationUrl(string slug, string playerName);
        string GetPlayerDeleteUrl(Homegame homegame, Player player);
        string GetPlayerDetailsUrl(string slug, string playerName);
        string GetPlayerInviteUrl(Homegame homegame, Player player);

        string GetCashgameDeleteUrl(string slug, string dateStr);
        string GetCashgameDetailsChartJsonUrl(string slug, string dateStr);
        string GetCashgameDetailsUrl(string slug, string dateStr);
        string GetCashgameEditUrl(string slug, string dateStr);

        string GetCashgameActionChartJsonUrl(string slug, string dateStr, string playerName);
        string GetCashgameActionUrl(string slug, string dateStr, string playerName);
        
        string GetCashgameCheckpointDeleteUrl(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint);
        
    }
}
