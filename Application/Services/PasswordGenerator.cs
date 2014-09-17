namespace Application.Services
{
    public static class PasswordGenerator
    {
        private const int PasswordLength = 8;
	    private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

        public static string CreatePassword(){
            return RandomStringGenerator.GetString(PasswordLength, AllowedCharacters);
		}
	}
}