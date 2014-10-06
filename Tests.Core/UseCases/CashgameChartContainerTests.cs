using Core.Urls;
using Core.UseCases.CashgameChartContainer;
using NUnit.Framework;

namespace Tests.Core.UseCases
{
    class CashgameChartContainerTests
    {
        [Test]
        public void CashgameChartContainer_DateUrlIsSet()
        {
            const string slug = "a";
            const int year = 1;

            var request = new CashgameChartContainerRequest(slug, year);
            var result = Execute(request);

            Assert.IsInstanceOf<CashgameChartJsonUrl>(result.DataUrl);
        }

        private CashgameChartContainerResult Execute(CashgameChartContainerRequest request)
        {
            return CashgameChartContainerInteractor.Execute(request);
        }
    }
}
