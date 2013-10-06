using Core.Classes;
using Infrastructure.Data.Classes;

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

        public User Create(RawUser rawUser)
        {
            return new User
            {
                Id = rawUser.Id,
                UserName = rawUser.UserName,
                DisplayName = rawUser.DisplayName,
                RealName = rawUser.RealName,
                Email = rawUser.Email,
                GlobalRole = (Role)rawUser.GlobalRole
            };
        }
    }

}