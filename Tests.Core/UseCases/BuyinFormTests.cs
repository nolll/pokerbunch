using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.BuyinForm;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class BuyinFormTests : TestBase
    {
        private const string Slug = "a";
        private const int PlayerIdInGame = 1;
        private const int PlayerIdOther = 2;
        private const int BunchId = 3;

        [TestCase(1)]
        [TestCase(2)]
        public void BuyinForm_BuyinAmountIsSetFromBunchDefaultAmount(int defaultBuyin)
        {
            SetupBunch(defaultBuyin);
            SetupGameAndPlayer(PlayerIdInGame);

            var result = Execute(CreateRequest());

            Assert.AreEqual(defaultBuyin, result.BuyinAmount);
        }

        [Test]
        public void BuyinForm_PlayerIsNotInGame_CanEnterStackIsFalse()
        {
            SetupBunch();
            SetupGameAndPlayer(PlayerIdOther);

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanEnterStack);
        }

        [Test]
        public void BuyinForm_PlayerIsInGame_CanEnterStackIsTrue()
        {
            SetupBunch();
            SetupGameAndPlayer(PlayerIdInGame);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEnterStack);
        }

        private BuyinFormRequest CreateRequest()
        {
            return new BuyinFormRequest(Slug, PlayerIdInGame);
        }

        private void SetupBunch(int defaultBuyin = 0)
        {
            var bunch = A.Bunch.WithId(BunchId).WithDefaultBuyin(defaultBuyin).Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private void SetupGameAndPlayer(int playerId)
        {
            var result = A.CashgameResult.WithPlayerId(playerId).Build();
            var results = new List<CashgameResult>{result};
            var game = new CashgameInTest(results: results);
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(BunchId)).Returns(game);
        }

        private BuyinFormResult Execute(BuyinFormRequest request)
        {
            return BuyinFormInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}
