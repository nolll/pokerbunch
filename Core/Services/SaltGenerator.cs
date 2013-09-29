namespace Core.Services{

	public class SaltGenerator : ISaltGenerator
	{
	    private const int SaltLength = 10;
	    private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

	    public string CreateSalt(){
			var stringGenerator = new RandomStringGenerator(SaltLength, AllowedCharacters);
			return stringGenerator.GetString();
		}

	}

}