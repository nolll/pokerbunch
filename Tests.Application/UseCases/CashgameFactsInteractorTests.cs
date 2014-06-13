using Application.UseCases.CashgameFacts;
using Core.Repositories;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class CashgameFactsInteractorTests : MockContainer
    {
        //[TestCase(null)]
        //[TestCase(1)]
        //public void GetFacts_WithOrWithoutYear_ReturnsSuite(int? year)
        //{
        //    var homegame = new FakeHomegame();
        //    var players = new List<Player>();
        //    var cashgames = new List<Cashgame>();
        //    var facts = new FakeFactBuilder();

        //    GetMock<IPlayerRepository>().Setup(o => o.GetList(homegame)).Returns(players);
        //    GetMock<ICashgameRepository>().Setup(o => o.GetPublished(homegame, year)).Returns(cashgames);

        //    var result = Sut.Execute(homegame, year);

        //    Assert.AreEqual(facts, result);
        //}

        private CashgameFactsInteractor Sut
        {
            get
            {
                return new CashgameFactsInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object,
                    GetMock<IPlayerRepository>().Object);
            }
        }
    }
}
