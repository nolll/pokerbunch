using System;
using System.Text;

namespace Core.Services{

	public class RandomStringGenerator {

		private readonly int _stringLength;
		private readonly string _allowedCharacters;

		public RandomStringGenerator(int stringLength, string allowedCharacters){
			_stringLength = stringLength;
			_allowedCharacters = allowedCharacters;
		}

		public string GetString()
		{
		    var random = new Random();
		    var max = _allowedCharacters.Length - 1;
		    var str = new StringBuilder();
			for(var i = 0; i < _stringLength; i++)
			{
			    var randomPos = random.Next(max);
			    str.Append(_allowedCharacters.Substring(randomPos, 1));
			}
			return str.ToString();
		}

	}

}