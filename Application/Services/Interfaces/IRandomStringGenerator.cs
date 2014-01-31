namespace Application.Services
{
    public interface IRandomStringGenerator
    {
        string GetString(int stringLength, string allowedCharacters);
    }
}