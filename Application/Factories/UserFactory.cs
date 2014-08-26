using Core.Entities;

namespace Application.Factories
{
    public static class UserFactory
    {
        public static User Create(
            int id,
            string userName,
            string displayName,
            string realName,
            string email,
            Role globalRole,
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
                encryptedPassword,
                salt
            );
        }
    }
}