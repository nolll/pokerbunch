using NUnit.Framework;

namespace Tests.Core.UseCases.BunchContextTests
{
    public class WithoutBunchIdWithOneBunch : Arrange
    {
        protected override string BunchId => null;

        [Test]
        public void HasBunchIsTrue()
        {
            Assert.IsTrue(Result.HasBunch);
        }
    }
}