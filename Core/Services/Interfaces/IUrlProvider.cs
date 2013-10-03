using Core.Classes;

namespace Core.Services
{
    public interface IUrlProvider
    {
        string GetAddUserUrl();
        string GetAuthLoginUrl();
        string GetJoinHomegameUrl(Homegame homegame);
        string GetTwitterCallbackUrl();
    }
}
