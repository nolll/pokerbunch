namespace Application.Services.Interfaces
{
    public interface IPasswordMessageBuilder
    {
        string GetSubject();
        string GetBody(string password);
    }
}