using System.Collections.Generic;
using Core.Repositories;
using Core.UseCases.CashgameHome;
using Moq;
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
            var result = Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/add", result.StartUrl.Relative);
        }

        [Test]
        public void Execute_WithYears_YearsContainsThemAndLatestYearIsCorrect()
        {
            var request = new CashgameHomeRequest(Constants.SlugA);
            var result = Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/matrix/2002", result.StartUrl.Relative);
        }

        private CashgameHomeResult Execute(CashgameHomeRequest request)
        {
            return CashgameHomeInteractor.Execute(
                request,
                Repos.Bunch,
                Repos.Cashgame);
        }
    }
}