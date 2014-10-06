namespace Core.Services.Interfaces
{
    public interface IRandomService
    {
        string GetPasswordCharacters();
        string GetSaltCharacters();
    }
}