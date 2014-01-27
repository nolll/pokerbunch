namespace App.Services.Interfaces
{
    public interface IRegistrationConfirmationMessageBuilder
    {
        string GetSubject();
        string GetBody(string password);
    }
}