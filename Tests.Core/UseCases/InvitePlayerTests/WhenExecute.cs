using NUnit.Framework;

namespace Tests.Core.UseCases.InvitePlayerTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void PlayerIdIsSet()
        {
            Assert.AreEqual(PlayerId, Result.PlayerId);
        }

        [Test]
        public void CallsInvite()
        {
            Assert.AreEqual(PlayerId, PostedPlayerId);
            Assert.AreEqual(Email, PostedEmail);
        }
    }
}
