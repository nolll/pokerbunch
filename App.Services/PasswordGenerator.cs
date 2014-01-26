using App.Services.Interfaces;

namespace App.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private readonly IRandomStringGenerator _randomStringGenerator;
        private const int PasswordLength = 8;
	    private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

        public PasswordGenerator(IRandomStringGenerator randomStringGenerator)
        {
            _randomStringGenerator = randomStringGenerator;
        }

        public string CreatePassword(){
            return _randomStringGenerator.GetString(PasswordLength, AllowedCharacters);
		}

	}

}