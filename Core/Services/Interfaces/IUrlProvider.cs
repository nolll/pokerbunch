using Core.Classes;

namespace Core.Services
{
    public interface IUrlProvider
    {
        string GetLogoutUrl();
        string GetAddUserUrl();
        string GetLoginUrl();
        string GetJoinHomegameUrl(Homegame homegame);
        string GetTwitterCallbackUrl();
    }
}
