using Core.Entities;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class UserBuilder
    {
        private int _id;
        private string _userName;
        private string _displayName;
        private Role _role;

        public UserBuilder()
        {
            _id = 1;
            _userName = "a";
            _displayName = "b";
            _role = Role.None;
        }

        public User Build()
        {
            return new UserInTest(_id, _userName, _displayName, globalRole: _role);
        }

        public UserBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public UserBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public UserBuilder IsAdmin()
        {
            _role = Role.Admin;
            return this;
        }
    }
}