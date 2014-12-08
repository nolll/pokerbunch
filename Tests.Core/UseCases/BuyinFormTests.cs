using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.BuyinForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class BuyinFormTests : TestBase
    {
        private const int PlayerIdInGame = 1;
        private const int PlayerIdOther = 2;

        [Test]
        public void BuyinForm_BuyinAmountIsSetFromBunchDefaultAmount()
        {
            SetupGameAndPlayer(PlayerIdInGame);

            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.DefaultBuyinA, result.BuyinAmount);
        }

        [Test]
        public void BuyinForm_PlayerIsNotInGame_CanEnterStackIsFalse()
        {
            SetupGameAndPlayer(PlayerIdOther);

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanEnterStack);
        }

        [Test]
        public void BuyinForm_PlayerIsInGame_CanEnterStackIsTrue()
        {
            SetupGameAndPlayer(PlayerIdInGame);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEnterStack);
        }

        private BuyinFormRequest CreateRequest()
        {
            return new BuyinFormRequest(Constants.SlugA, PlayerIdInGame);
        }

        private void SetupGameAndPlayer(int playerId)
        {
            var result = A.CashgameResult.WithPlayerId(playerId).Build();
            var results = new List<CashgameResult>{result};
            var game = A.Cashgame.WithResults(results).Build();
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(Constants.BunchIdA)).Returns(game);
        }

        private BuyinFormResult Execute(BuyinFormRequest request)
        {
            return BuyinFormInteractor.Execute(
                Repos.Bunch,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}
