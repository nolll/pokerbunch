using System;
using System.Text;

namespace Application.Services{

	public class RandomStringGenerator : IRandomStringGenerator
	{
	    private readonly Random _random;

		public RandomStringGenerator(){
            _random = new Random();
		}

		public string GetString(int stringLength, string allowedCharacters)
		{
            if (string.IsNullOrEmpty(allowedCharacters))
            {
                return string.Empty;
            }
		    var max = allowedCharacters.Length - 1;
		    var str = new StringBuilder();
			for(var i = 0; i < stringLength; i++)
			{
			    var randomPos = _random.Next(max);
			    str.Append(allowedCharacters.Substring(randomPos, 1));
			}
			return str.ToString();
		}

	}

}