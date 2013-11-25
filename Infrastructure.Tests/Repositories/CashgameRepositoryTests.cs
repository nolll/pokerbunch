using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.System;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Repositories{

	public class CashgameRepositoryTests : MockContainer {

		[Test]
		public void StartGame_CallsUpdateWithRawCashgame(){
			var cashgame = new FakeCashgame();
            var rawCashgame = new RawCashgameWithResults();

            GetMock<IRawCashgameFactory>().Setup(o => o.Create(cashgame, GameStatus.Running)).Returns(rawCashgame);

            var sut = GetSut();
			sut.StartGame(cashgame);

            GetMock<ICashgameStorage>().Verify(o => o.UpdateGame(rawCashgame));
		}

		[Test]
		public void EndGame_CallsUpdateGameAndSetsCurrentDateAndStatusPublished(){
            var cashgame = new FakeCashgame();
            var rawCashgame = new RawCashgameWithResults();

            GetMock<IRawCashgameFactory>().Setup(o => o.Create(cashgame, GameStatus.Published)).Returns(rawCashgame);

            var sut = GetSut();
			sut.EndGame(cashgame);

            GetMock<ICashgameStorage>().Verify(o => o.UpdateGame(rawCashgame));
		}

        private CashgameRepository GetSut()
        {
            return new CashgameRepository(
                GetMock<ICashgameStorage>().Object,
                GetMock<ICashgameFactory>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameResultFactory>().Object,
                GetMock<IRawCashgameFactory>().Object,
                GetMock<ICheckpointFactory>().Object,
                CacheContainerFake,
                GetMock<ICheckpointStorage>().Object,
                GetMock<ITimeProvider>().Object);
        }

	}

}