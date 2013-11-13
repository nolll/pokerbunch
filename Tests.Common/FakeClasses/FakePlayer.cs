using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakePlayer : Player
    {
        public FakePlayer(
            int id = default(int),
            int userId = default(int),
            string displayName = default(string),
            Role role = default(Role))
            : base(id, userId, displayName, role)
        {
        }
    }
}