using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Repositories;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Repositories{

	public class CashgameRepositoryTests : InfrastructureMockContainer {

		[Test]
		public void StartGame_CallsUpdateWithRawCashgame(){
			var cashgame = new FakeCashgame();
            var rawCashgame = new RawCashgameWithResults();

            Mocks.RawCashgameFactoryMock.Setup(o => o.Create(cashgame, GameStatus.Running)).Returns(rawCashgame);

            var sut = GetSut();
			sut.StartGame(cashgame);

            Mocks.CashgameStorageMock.Verify(o => o.UpdateGame(rawCashgame));
		}

		[Test]
		public void EndGame_CallsUpdateGameAndSetsCurrentDateAndStatusPublished(){
            var cashgame = new FakeCashgame();
            var rawCashgame = new RawCashgameWithResults();

            Mocks.RawCashgameFactoryMock.Setup(o => o.Create(cashgame, GameStatus.Published)).Returns(rawCashgame);

            var sut = GetSut();
			sut.EndGame(cashgame);

            Mocks.CashgameStorageMock.Verify(o => o.UpdateGame(rawCashgame));
		}

        private CashgameRepository GetSut()
        {
            return new CashgameRepository(
                Mocks.CashgameStorageMock.Object,
                Mocks.CashgameFactoryMock.Object,
                Mocks.PlayerRepositoryMock.Object,
                Mocks.CashgameSuiteFactoryMock.Object,
                Mocks.CashgameResultFactoryMock.Object,
                Mocks.RawCashgameFactoryMock.Object,
                Mocks.CheckpointFactoryMock.Object,
                Mocks.CacheContainerFake,
                Mocks.CheckpointStorageMock.Object,
                Mocks.TimeProviderMock.Object);
        }

	}

}