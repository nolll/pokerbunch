using NUnit.Framework;

namespace Tests.Core.UseCases.MatrixTests
{
    public class WithTwoGamesOnDifferentYears : Arrange
    {
        protected override bool DifferentYears => true;

        [Test]
        public void Matrix_WithTwoGamesOnDifferentYears_SpansMultipleYearsIsTrue()
        {
            Assert.IsTrue(Result.SpansMultipleYears);
        }
    }
}