namespace Application.Services{

	public interface IEncryptionService {
	    string Encrypt(string str, string salt);
	    string GetSha1Hash(string input);
	    string GetMd5Hash(string input);
	}

}