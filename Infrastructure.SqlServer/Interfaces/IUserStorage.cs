using System.Collections.Generic;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer.Interfaces {

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