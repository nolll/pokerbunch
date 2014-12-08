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
            var years = new List<int>();
            var request = new CashgameHomeRequest(Constants.SlugA);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/add", result.StartUrl.Relative);
        }

        [Test]
        public void Execute_WithYears_YearsContainsThemAndLatestYearIsCorrect()
        {
            var years = new List<int> { 1, 2, 3 };
            var request = new CashgameHomeRequest(Constants.SlugA);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/matrix/3", result.StartUrl.Relative);
        }

        private CashgameHomeResult Execute(CashgameHomeRequest request)
        {
            return CashgameHomeInteractor.Execute(
                request,
                Repos.Bunch,
                GetMock<ICashgameRepository>().Object);
        }
    }
}