namespace Application.Services
{
    public interface IPasswordMessageBuilder
    {
        string GetSubject();
        string GetBody(string password);
    }
}