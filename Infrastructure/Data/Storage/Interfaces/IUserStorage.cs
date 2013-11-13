using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IUserStorage
    {
        RawUser GetUserById(int id);
        int? GetUserIdByEmail(string email);
        int? GetUserIdByName(string userName);
        int? GetUserIdByToken(string token);
        int? GetUserIdByCredentials(string userNameOrEmail, string password);
		IList<RawUser> GetUsers(IList<int> ids);
        IList<int> GetUserIds();
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