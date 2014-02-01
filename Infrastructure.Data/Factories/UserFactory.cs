using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;

namespace Infrastructure.Data.Factories{

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
                (Role)rawUser.GlobalRole,
                rawUser.Token,
                rawUser.EncryptedPassword,
                rawUser.Salt
            );
        }
    }

}