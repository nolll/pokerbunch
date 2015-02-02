using Core.UseCases.CashgameHome;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class CashgameHomeTests : TestBase
    {
        [Test]
        public void Execute_WithoutYears_StartUrlIsAddPage()
        {
            Repos.Cashgame.ClearList();

            var request = new CashgameHomeRequest(Constants.SlugA);
            var result = Sut.Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/add", result.StartUrl.Relative);
        }

        [Test]
        public void Execute_WithYears_YearsContainsThemAndLatestYearIsCorrect()
        {
            var request = new CashgameHomeRequest(Constants.SlugA);
            var result = Sut.Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/matrix/2002", result.StartUrl.Relative);
        }

        private CashgameHomeInteractor Sut
        {
            get
            {
                return new CashgameHomeInteractor(
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }
    }
}