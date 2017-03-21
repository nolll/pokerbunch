using NUnit.Framework;

namespace Tests.Core.UseCases.JoinBunchTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void BunchIdIsSet()
        {
            Assert.AreEqual(BunchId, Result.BunchId);
        }

        [Test]
        public void CallsJoin()
        {
            Assert.AreEqual(BunchId, PostedBunchId);
            Assert.AreEqual(Code, PostedCode);
        }
    }
}