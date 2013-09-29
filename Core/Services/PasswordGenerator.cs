namespace Core.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
	    private const int PasswordLength = 8;
	    private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

	    public string CreatePassword(){
			var stringGenerator = new RandomStringGenerator(PasswordLength, AllowedCharacters);
			return stringGenerator.GetString();
		}

	}

}