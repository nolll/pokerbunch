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
        string GetHomegameListingUrl();
        string GetHomeUrl();
        string GetSharingSettingsUrl();
        string GetTwitterSettingsUrl();
        string GetTwitterStartShareUrl();
        string GetTwitterStopShareUrl();
        string GetUserAddConfirmationUrl();
        string GetUserListingUrl();

        string GetUserDetailsUrl(User user);
        string GetUserEditUrl(User user);
        
        string GetJoinHomegameUrl(Homegame homegame);
        string GetCashgameAddUrl(Homegame homegame);
        string GetCashgameEndUrl(Homegame homegame);
        string GetCashgameIndexUrl(Homegame homegame);
        string GetHomegameDetailsUrl(Homegame homegame);
        string GetHomegameEditUrl(Homegame homegame);
        string GetHomegameJoinConfirmationUrl(Homegame homegame);
        string GetPlayerAddUrl(Homegame homegame);
        string GetPlayerIndexUrl(Homegame homegame);
        string GetRunningCashgameUrl(Homegame homegame);
        string GetPlayerAddConfirmationUrl(Homegame homegame);
        
        string GetCashgameChartJsonUrl(Homegame homegame, int? year);
        string GetCashgameChartUrl(Homegame homegame, int? year);
        string GetCashgameFactsUrl(Homegame homegame, int? year);
        string GetCashgameLeaderboardUrl(Homegame homegame, int? year);
        string GetCashgameListingUrl(Homegame homegame, int? year);
        string GetCashgameMatrixUrl(Homegame homegame, int? year);

        string GetCashgameBuyinUrl(Homegame homegame, Player player);
        string GetCashgameCashoutUrl(Homegame homegame, Player player);
        string GetCashgameReportUrl(Homegame homegame, Player player);
        string GetPlayerInviteConfirmationUrl(Homegame homegame, Player player);
        string GetPlayerDeleteUrl(Homegame homegame, Player player);
        string GetPlayerDetailsUrl(Homegame homegame, Player player);
        string GetPlayerInviteUrl(Homegame homegame, Player player);

        string GetCashgameDeleteUrl(Homegame homegame, Cashgame cashgame);
        string GetCashgameDetailsChartJsonUrl(Homegame homegame, Cashgame cashgame);
        string GetCashgameDetailsUrl(Homegame homegame, Cashgame cashgame);
        string GetCashgameEditUrl(Homegame homegame, Cashgame cashgame);

        string GetCashgameActionChartJsonUrl(Homegame homegame, Cashgame cashgame, Player player);
        string GetCashgameActionUrl(Homegame homegame, Cashgame cashgame, Player player);
        
        string GetCashgameCheckpointDeleteUrl(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint);
        
    }
}
