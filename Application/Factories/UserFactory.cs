using Core.Classes;

namespace Application.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(
            int id,
            string userName,
            string displayName,
            string realName,
            string email,
            Role globalRole,
            string token,
            string encryptedPassword,
            string salt)
        {
            return new User
            (
                id,
                userName,
                displayName,
                realName,
                email,
                globalRole,
                token,
                encryptedPassword,
                salt
            );
        }
    }
}