namespace Core.Services{

	public class SaltGenerator : ISaltGenerator
	{
	    private readonly IRandomStringGenerator _randomStringGenerator;
	    private const int SaltLength = 10;
	    private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

	    public SaltGenerator(IRandomStringGenerator randomStringGenerator)
	    {
	        _randomStringGenerator = randomStringGenerator;
	    }

	    public string CreateSalt(){
            return _randomStringGenerator.GetString(SaltLength, AllowedCharacters);
		}

	}

}