using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services{

	class EncryptionService : IEncryptionService{

		public string Encrypt(string str, string salt)
		{
		    var input = str + salt;
            return string.Join("", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input)).Select(x => x.ToString("X2"))).ToLower();
		}

	}

}