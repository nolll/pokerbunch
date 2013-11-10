using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakePlayer : Player
    {
        public FakePlayer(
            int id = default(int),
            string userName = default(string),
            string displayName = default(string),
            Role role = default(Role))
            : base(id, userName, displayName, role)
        {
        }
    }
}