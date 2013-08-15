using Core.Classes;

namespace Infrastructure.Factories{

    public class UserFactory : IUserFactory{

		public User Create(int id, string userName, string displayName, string realName, string email, Role globalRole){
			return new User
			    {
			        Id = id,
			        UserName = userName,
			        DisplayName = displayName,
			        RealName = realName,
			        Email = email,
			        GlobalRole = globalRole
			    };
		}

	}

}