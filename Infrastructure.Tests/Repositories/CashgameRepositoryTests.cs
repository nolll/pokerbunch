using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Repositories;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Repositories{

	public class CashgameRepositoryTests : MockContainer {

		[Test]
		public void StartGame_CallsUpdateWithRawCashgame(){
			var cashgame = new Cashgame();
		    var rawCashgame = new RawCashgame();

		    RawCashgameFactoryMock.Setup(o => o.Create(cashgame, GameStatus.Running)).Returns(rawCashgame);

            var sut = GetSut();
			sut.StartGame(cashgame);

            CashgameStorageMock.Verify(o => o.UpdateGame(rawCashgame));
		}

		[Test]
		public void EndGame_CallsUpdateGameAndSetsCurrentDateAndStatusPublished(){
            var cashgame = new Cashgame();
            var rawCashgame = new RawCashgame();

            RawCashgameFactoryMock.Setup(o => o.Create(cashgame, GameStatus.Published)).Returns(rawCashgame);

            var sut = GetSut();
			sut.EndGame(cashgame);

            CashgameStorageMock.Verify(o => o.UpdateGame(rawCashgame));
		}

        private CashgameRepository GetSut()
        {
            return new CashgameRepository(
                CashgameStorageMock.Object, 
                CashgameFactoryMock.Object, 
                PlayerRepositoryMock.Object, 
                CashgameSuiteFactoryMock.Object, 
                CashgameResultFactoryMock.Object, 
                CheckpointRepositoryMock.Object,
                RawCashgameFactoryMock.Object);
        }

	}

}