using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services{
    public class EncryptionService : IEncryptionService{

		public string Encrypt(string str, string salt)
		{
            return GetSha1Hash(str + salt);
		}

        public string GetSha1Hash(string input)
        {
            return string.Join("", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input)).Select(x => x.ToString("X2"))).ToLower();
        }

        /*
        public string GetMd5Hash(string input)
        {
            return string.Join("", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input)).Select(x => x.ToString("X2"))).ToLower();
        }
        */

        public string GetMd5Hash(string input)
        {
            var sb = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input.Trim()));
                foreach (var inputByte in inputBytes)
                {
                    sb.Append(inputByte.ToString("x2"));
                }
            }
            return sb.ToString();
        }

	}

}