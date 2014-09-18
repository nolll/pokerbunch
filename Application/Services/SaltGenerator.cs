namespace Application.Services
{
	public static class SaltGenerator
	{
	    private const int SaltLength = 10;
	    private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

        public static string CreateSalt()
        {
            return CreateSalt(AllowedCharacters);
        }

        public static string CreateSalt(string saltCharacters)
        {
            return RandomStringGenerator.GetString(SaltLength, saltCharacters);
        }
	}
}