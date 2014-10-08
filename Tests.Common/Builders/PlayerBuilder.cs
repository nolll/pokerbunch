using Core.Entities;

namespace Tests.Common.Builders
{
    public class PlayerBuilder
    {
        private int _id;
        private int _userId;
        private string _displayName;
        private Role _role;

        public PlayerBuilder()
        {
            _id = 0;
            _userId = 0;
            _displayName = "";
            _role = Role.None;
        }

        public Player Build()
        {
            return new Player(_id, _userId, _displayName, _role);
        }

        public PlayerBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public PlayerBuilder WithUserId(int userId)
        {
            _userId = userId;
            return this;
        }

        public PlayerBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public PlayerBuilder WithRole(Role role)
        {
            _role = role;
            return this;
        }
    }
}