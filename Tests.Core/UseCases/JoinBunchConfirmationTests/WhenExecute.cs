using NUnit.Framework;

namespace Tests.Core.UseCases.JoinBunchConfirmationTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void JoinBunchConfirmation_BunchNameIsSet()
        {
            Assert.AreEqual(DisplayName, Result.BunchName);
        }

        [Test]
        public void JoinBunchConfirmation_BunchDetailsUrlIsSet()
        {
            Assert.AreEqual(BunchId, Result.BunchId);
        }
    }
}
