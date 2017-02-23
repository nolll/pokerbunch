using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithoutYearAndAnyPageButOverview : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Unknown;

        [Test]
        public void Execute_WithoutYear_SelectedYearIsNull()
        {
            var result = Sut.Execute(Request);

            Assert.IsNull(result.SelectedYear);
        }
    }
}