using System.Collections.Generic;
using Application.UseCases.BuyinForm;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BuyinFormTests : MockContainer
    {
        private const string Slug = "a";
        private const int PlayerIdInGame = 1;
        private const int PlayerIdOther = 2;
        private const int BunchId = 3;

        [TestCase(1)]
        [TestCase(2)]
        public void BuyinForm_BuyinAmountIsSetFromHomegameDefaultAmount(int defaultBuyin)
        {
            SetupHomegame(defaultBuyin);
            SetupGameAndPlayer(PlayerIdInGame);

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(defaultBuyin, result.BuyinAmount);
        }

        [Test]
        public void BuyinForm_PlayerIsNotInGame_CanEnterStackIsFalse()
        {
            SetupHomegame();
            SetupGameAndPlayer(PlayerIdOther);

            var result = Sut.Execute(CreateRequest());

            Assert.IsFalse(result.CanEnterStack);
        }

        [Test]
        public void BuyinForm_PlayerIsInGame_CanEnterStackIsTrue()
        {
            SetupHomegame();
            SetupGameAndPlayer(PlayerIdInGame);

            var result = Sut.Execute(CreateRequest());

            Assert.IsTrue(result.CanEnterStack);
        }

        private BuyinFormRequest CreateRequest()
        {
            return new BuyinFormRequest(Slug, PlayerIdInGame);
        }

        private void SetupHomegame(int defaultBuyin = 0)
        {
            var homegame = new HomegameInTest(id: BunchId, defaultBuyin: defaultBuyin);
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(Slug)).Returns(homegame);
        }

        private void SetupGameAndPlayer(int playerId)
        {
            var result = new CashgameResultInTest(playerId);
            var results = new List<CashgameResult>{result};
            var game = new CashgameInTest(results: results);
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(BunchId)).Returns(game);
        }

        private BuyinFormInteractor Sut
        {
            get
            {
                return new BuyinFormInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object);
            }
        }
    }
}
