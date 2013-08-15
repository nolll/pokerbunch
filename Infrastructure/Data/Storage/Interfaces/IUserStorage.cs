using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IUserStorage{

		User GetUserByEmail(string email);
		User GetUserByName(string userName);
		User GetUserByToken(string token);
		User GetUserByCredentials(string userNameOrEmail, string password);
		List<User> GetUsers();
		bool UpdateUser(User user);
		int AddUser(User user);
		bool DeleteUser(User user);
        string GetSalt(string userNameOrEmail);
		bool SetSalt(User user, string salt);
		bool SetEncryptedPassword(User user, string encryptedPassword);
		string GetToken(User user);
		bool SetToken(User user, string token);

	}

}