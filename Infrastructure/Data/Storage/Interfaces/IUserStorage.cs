using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IUserStorage{

		RawUser GetUserByEmail(string email);
        RawUser GetUserByName(string userName);
        RawUser GetUserByToken(string token);
        RawUser GetUserByCredentials(string userNameOrEmail, string password);
		List<RawUser> GetUsers();
		bool UpdateUser(RawUser user);
		int AddUser(RawUser user);
		bool DeleteUser(int userId);
        string GetSalt(string userNameOrEmail);
		bool SetSalt(string userName, string salt);
		bool SetEncryptedPassword(string userName, string encryptedPassword);
		string GetToken(string userName);
		bool SetToken(string userName, string token);

	}

}