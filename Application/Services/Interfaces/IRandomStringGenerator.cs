namespace App.Services.Interfaces
{
    public interface IRandomStringGenerator
    {
        string GetString(int stringLength, string allowedCharacters);
    }
}