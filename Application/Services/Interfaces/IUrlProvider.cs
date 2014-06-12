namespace Application.Services
{
    public interface IUrlProvider
    {
        string GetAddUserUrl();
        string GetLoginUrl();
        string GetTwitterCallbackUrl();
        string GetJoinHomegameUrl(string slug);
    }
}
