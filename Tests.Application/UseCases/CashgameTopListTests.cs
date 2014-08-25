using System.Collections.Generic;
using Application.UseCases.CashgameTopList;
using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    //todo: Needs a lot more tests
    class CashgameTopListTests : MockContainer
    {
        [Test]
        public void Execute_WithSlug_ReturnsTopListItems()
        {
            const string slug = "a";
            var homegame = new HomegameInTest();
            var totalResult = new CashgameTotalResultInTest(player: new PlayerInTest());
            var totalResultList = new List<CashgameTotalResult> {totalResult};
            var playerList = new List<Player>();
            var suite = new CashgameSuiteInTest(totalResults: totalResultList, players: playerList);
            var request = new TopListRequest(slug, ToplistSortOrder.Winnings, null);

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameService>().Setup(o => o.GetSuite(homegame, null)).Returns(suite);

            var result = Sut.Execute(request);

            Assert.IsInstanceOf<TopListResult>(result);
        }

        private TopListInteractor Sut
        {
            get
            {
                return new TopListInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameService>().Object);
            }
        }
    }
}
