using Core.Entities;
using Core.Services;

namespace Tests.Common.FakeServices
{
    public class FakeAuth : IAuth
    {
        public void SignIn(UserIdentity user, bool createPersistentCookie)
        {
            throw new System.NotImplementedException();
        }

        public void SignOut()
        {
            throw new System.NotImplementedException();
        }

        public User CurrentUser
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsInRole(string slug, Role manager)
        {
            throw new System.NotImplementedException();
        }
    }
}