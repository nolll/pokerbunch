using Core.Entities;

namespace Tests.Common.Builders
{
    public class UserBuilder
    {
        private int _id;
        private string _userName;
        private string _displayName;
        private string _realName;
        private string _email;
        private Role _globalRole;
        private string _encryptedPassword;
        private string _salt;

        public UserBuilder()
        {
            _id = 0;
            _userName = "";
            _displayName = "";
            _realName = "";
            _email = "";
            _globalRole = Role.None;
            _encryptedPassword = "";
            _salt = "";
        }

        public User Build()
        {
            return new User(_id, _userName, _displayName, _realName, _email, _globalRole, _encryptedPassword, _salt);
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

        public UserBuilder WithRealName(string realName)
        {
            _realName = realName;
            return this;
        }

        public   UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithGlobalRole(Role role)
        {
            _globalRole = role;
            return this;
        }

        public UserBuilder WithEncryptedPassword(string encryptetPassword)
        {
            _encryptedPassword = encryptetPassword;
            return this;
        }

        public UserBuilder WithSalt(string salt)
        {
            _salt = salt;
            return this;
        }
    }
}