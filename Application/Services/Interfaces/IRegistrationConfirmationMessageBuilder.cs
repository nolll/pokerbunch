namespace Application.Services
{
    public interface IRegistrationConfirmationMessageBuilder
    {
        string GetSubject();
        string GetBody(string password);
    }
}