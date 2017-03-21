using NUnit.Framework;

namespace Tests.Core.UseCases.BunchContextTests
{
    public class WithoutBunchIdWithoutBunches : Arrange
    {
        protected override string BunchId => null;

        [Test]
        public void HasBunchIsFalse()
        {
            Assert.IsFalse(Result.HasBunch);
        }
    }
}