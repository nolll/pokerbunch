using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public static class EncryptionService
    {
		public static string GetMd5Hash(string input)
        {
            var sb = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                var inputBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input.Trim()));
                foreach (var inputByte in inputBytes)
                {
                    sb.Append(inputByte.ToString("x2"));
                }
            }
            return sb.ToString();
        }
	}
}