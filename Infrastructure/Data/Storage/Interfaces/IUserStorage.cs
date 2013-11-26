using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IUserStorage
    {
        RawUser GetUserById(int id);
        int? GetUserIdByToken(string token);
		IList<RawUser> GetUsers(IEnumerable<int> ids);
        IList<int> GetUserIds();
		bool UpdateUser(RawUser user);
		int AddUser(RawUser user);
        int? GetUserIdByNameOrEmail(string userNameOrEmail);
    }

}