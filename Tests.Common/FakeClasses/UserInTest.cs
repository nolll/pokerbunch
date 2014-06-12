using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class UserInTest : User
    {
        public UserInTest(
            int id = default(int),
            string userName = default(string),
            string displayName = default(string),
            string realName = default(string),
            string email = default(string), 
            Role globalRole = default(Role),
            string encryptedPassword = default(string),
            string salt = default(string)
            ) : base(
            id, 
            userName, 
            displayName, 
            realName, 
            email, 
            globalRole,
            encryptedPassword,
            salt)
        {
        }
    }
}