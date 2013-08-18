namespace Core.Services{

	public interface IEncryptionService {

		string Encrypt(string str, string salt);

	}

}