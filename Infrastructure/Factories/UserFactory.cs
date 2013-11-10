using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{

    public class UserFactory : IUserFactory{

        public User Create(RawUser rawUser)
        {
            return new User
            (
                rawUser.Id,
                rawUser.UserName,
                rawUser.DisplayName,
                rawUser.RealName,
                rawUser.Email,
                (Role)rawUser.GlobalRole
            );
        }
    }

}