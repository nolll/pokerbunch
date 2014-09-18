namespace Application.Services
{
    public interface IRandomService
    {
        string GetPasswordCharacters();
        string GetSaltCharacters();
    }
}