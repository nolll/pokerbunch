using NUnit.Framework;

namespace Tests.Core.UseCases.TestEmailTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void MessageIsSet()
        {
            Assert.AreEqual(Message, Result.Message);
        }
    }
}