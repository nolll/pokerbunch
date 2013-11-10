using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakeUser : User
    {
        public FakeUser(
            int id = default(int),
            string userName = default(string),
            string displayName = default(string),
            string realName = default(string),
            string email = default(string), 
            Role globalRole = default(Role)
            ) : base(
                id, 
                userName, 
                displayName, 
                realName, 
                email, 
                globalRole)
        {
        }
    }
}