using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class PlayerInTest : Player
    {
        public PlayerInTest(
            int id = default(int),
            int userId = default(int),
            string displayName = default(string),
            Role role = default(Role))
            : base(id, userId, displayName, role)
        {
        }
    }
}