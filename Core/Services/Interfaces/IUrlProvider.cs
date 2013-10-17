using Core.Classes;
using Core.Classes.Checkpoints;

namespace Core.Services
{
    public interface IUrlProvider
    {
        string GetLogoutUrl();
        string GetAddUserUrl();
        string GetLoginUrl();
        string GetJoinHomegameUrl(Homegame homegame);
        string GetTwitterCallbackUrl();
        string GetCashgameActionChartJsonUrl(Homegame homegame, Cashgame cashgame, Player player);
        string GetCashgameActionUrl(Homegame homegame, Cashgame cashgame, Player player);
        string GetCashgameAddUrl(Homegame homegame);
        string GetCashgameBuyinUrl(Homegame homegame, Player player);
        string GetCashgameCashoutUrl(Homegame homegame, Player player);
        string GetCashgameChartJsonUrl(Homegame homegame, int? year);
        string GetCashgameChartUrl(Homegame homegame, int? year);
        string GetCashgameCheckpointDeleteUrl(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint);
        string GetCashgameDeleteUrl(Homegame homegame, Cashgame cashgame);
        string GetCashgameDetailsChartJsonUrl(Homegame homegame, Cashgame cashgame);
        string GetCashgameDetailsUrl(Homegame homegame, Cashgame cashgame);
        string GetCashgameEditUrl(Homegame homegame, Cashgame cashgame);
        string GetCashgameEndUrl(Homegame homegame);
        string GetCashgameFactsUrl(Homegame homegame, int? year);
        string GetCashgameIndexUrl(Homegame homegame);
        string GetCashgameLeaderboardUrl(Homegame homegame, int? year);
        string GetCashgameListingUrl(Homegame homegame, int? year);
        string GetCashgameMatrixUrl(Homegame homegame, int? year);
        string GetCashgameReportUrl(Homegame homegame, Player player);
        string GetChangePasswordConfirmationUrl();
        string GetChangePasswordUrl();
        string GetForgotPasswordConfirmationUrl();
        string GetForgotPasswordUrl();
        string GetHomegameAddConfirmationUrl();
        string GetHomegameAddUrl();
        string GetHomegameDetailsUrl(Homegame homegame);
        string GetHomegameEditUrl(Homegame homegame);
        string GetHomegameJoinConfirmationUrl(Homegame homegame);
        string GetHomegameListingUrl();
        string GetHomeUrl();
        string GetPlayerAddConfirmationUrl(Homegame homegame);
        string GetPlayerAddUrl(Homegame homegame);
        string GetPlayerDeleteUrl(Homegame homegame, Player player);
        string GetPlayerDetailsUrl(Homegame homegame, Player player);
        string GetPlayerIndexUrl(Homegame homegame);
        string GetPlayerInviteConfirmationUrl(Homegame homegame, Player player);
        string GetPlayerInviteUrl(Homegame homegame, Player player);
        string GetRunningCashgameUrl(Homegame homegame);
        string GetSharingSettingsUrl();
        string GetTwitterSettingsUrl();
        string GetTwitterStartShareUrl();
        string GetTwitterStopShareUrl();
        string GetUserAddConfirmationUrl();
        string GetUserDetailsUrl(User user);
        string GetUserEditUrl(User user);
        string GetUserListingUrl();
    }
}
