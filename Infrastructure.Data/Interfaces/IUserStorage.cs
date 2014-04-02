using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Interfaces {

	public interface IUserStorage
    {
        RawUser GetUserById(int id);
		IList<RawUser> GetUserList(IList<int> ids);
        IList<int> GetUserIdList();
		bool UpdateUser(RawUser user);
		int AddUser(RawUser user);
        int? GetUserIdByNameOrEmail(string userNameOrEmail);
    }

}