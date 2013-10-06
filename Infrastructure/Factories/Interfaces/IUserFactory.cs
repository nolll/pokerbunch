using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{

	public interface IUserFactory{

		User Create(int id, string userName, string displayName, string realName, string email, Role globalRole);
	    User Create(RawUser rawUser);

	}

}