using Core.Classes;

namespace Core.Services
{
    public interface IUrlProvider
    {
        string GetJoinHomegameUrl(Homegame homegame);
        string GetAddUserUrl();
        string GetAuthLoginUrl();
    }
}
