using NUnit.Framework;

namespace Tests.Core.UseCases.DeleteCheckpointTests
{
    public class WithRunningGame : Arrange
    {
        protected override bool IsRunning => true;

        [Test]
        public void DeletesCheckpointAndReturnsCorrectValues()
        {
            Assert.IsTrue(Result.GameIsRunning);
        }
    }
}