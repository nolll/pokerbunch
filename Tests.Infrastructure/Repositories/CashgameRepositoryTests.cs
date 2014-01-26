using Core.Classes;
using Infrastructure.Caching;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Repositories{

	public class CashgameRepositoryTests : MockContainer {

		[Test]
		public void StartGame_CallsUpdateWithRawCashgame(){
			var cashgame = new FakeCashgame();
            var rawCashgame = new RawCashgameWithResults();

            GetMock<IRawCashgameFactory>().Setup(o => o.Create(cashgame, null)).Returns(rawCashgame);

            var sut = GetSut();
			sut.StartGame(cashgame);

            GetMock<ICashgameStorage>().Verify(o => o.UpdateGame(rawCashgame));
		}

		[Test]
		public void EndGame_CallsUpdateGameAndSetsCurrentDateAndStatusPublished()
		{
		    var homegame = new FakeHomegame();
            var cashgame = new FakeCashgame();
            var rawCashgame = new RawCashgameWithResults();

            GetMock<IRawCashgameFactory>().Setup(o => o.Create(cashgame, GameStatus.Published)).Returns(rawCashgame);

            var sut = GetSut();
			sut.EndGame(homegame, cashgame);

            GetMock<ICashgameStorage>().Verify(o => o.UpdateGame(rawCashgame));
		}

        private CashgameRepository GetSut()
        {
            return new CashgameRepository(
                GetMock<ICashgameStorage>().Object,
                GetMock<ICashgameFactory>().Object,
                GetMock<IRawCashgameFactory>().Object,
                CacheContainerFake,
                GetMock<ICheckpointStorage>().Object,
                GetMock<ICacheKeyProvider>().Object,
                GetMock<ICacheBuster>().Object);
        }

	}

}