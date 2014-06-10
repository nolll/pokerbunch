namespace Application.Services
{
    public interface IUrlProvider
    {
        string GetAddUserUrl();
        string GetLoginUrl();
        string GetTwitterCallbackUrl();
        string GetJoinHomegameUrl(string slug);
        string GetHomegameDetailsUrl(string slug);
        string GetHomegameEditUrl(string slug);
        string GetHomegameJoinConfirmationUrl(string slug);
        string GetPlayerAddUrl(string slug);
        string GetRunningCashgameUrl(string slug);
        string GetPlayerAddConfirmationUrl(string slug);
        string GetPlayerInviteConfirmationUrl(string slug, int playerId);
        string GetPlayerDeleteUrl(string slug, int playerId);
        string GetPlayerInviteUrl(string slug, int playerId);
    }
}
