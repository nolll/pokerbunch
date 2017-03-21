using NUnit.Framework;

namespace Tests.Core.UseCases.JoinBunchFormTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void DisplayNameIsSet()
        {
            Assert.AreEqual(DisplayName, Result.BunchName);
        }
    }
}
