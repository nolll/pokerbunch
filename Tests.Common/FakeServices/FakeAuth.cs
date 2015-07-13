using Core.Entities;
using Core.Services;

namespace Tests.Common.FakeServices
{
    public class FakeAuth : IAuth
    {
        private Role _currentRole;
        public UserIdentity UserIdentity { get; private set; }
        public bool StayLoggedIn { get; private set; }
        
        public void SignIn(UserIdentity user, bool createPersistentCookie)
        {
            UserIdentity = user;
            StayLoggedIn = createPersistentCookie;
        }

        public bool IsInRole(string slug, Role role)
        {
            return role <= _currentRole;
        }

        public void SetCurrentRole(Role role)
        {
            _currentRole = role;
        }

        public void Reset()
        {
            _currentRole = Role.None;
            UserIdentity = null;
            StayLoggedIn = false;
        }
    }
}