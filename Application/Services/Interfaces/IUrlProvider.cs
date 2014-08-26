namespace Application.Services
{
    public interface IUrlProvider
    {
        string GetAddUserUrl();
        string GetJoinHomegameUrl(string slug);
    }
}
