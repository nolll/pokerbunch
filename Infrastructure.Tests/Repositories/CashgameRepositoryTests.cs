using System;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Repositories{

	public class CashgameRepositoryTests : MockContainer {

		private CashgameRepository GetSut(){
			return new CashgameRepository(CashgameStorageMock.Object, CashgameFactoryMock.Object, PlayerStorageMock.Object, TimeProviderMock.Object, CashgameSuiteFactoryMock.Object, CashgameResultFactoryMock.Object);
		}

        [Test]
		public void StartGame_CallsUpdateGameAndSetsCurrentDateAndStatusRunning(){
			var id = 1;
			var location = "a";
			var status = GameStatus.Created;
			var cashgame = new Cashgame {Id = id, Location = location, Status = status};

            TimeProviderMock.Setup(o => o.GetTime()).Returns(DateTime.Parse("2001-01-01 01:00:00"));
			var expectedDate = "2001-01-01";
			var expectedStatus = GameStatus.Running;

            var recievedDate = "";
            var recievedStatus = GameStatus.Created;
            CashgameStorageMock.Setup(o => o.UpdateGame(It.IsAny<RawCashgame>()))
                                .Callback(
                                    (RawCashgame rawCashgame) => {
                                        recievedDate = rawCashgame.Date;
                                        recievedStatus = rawCashgame.Status;
                                });

            var sut = GetSut();
			sut.StartGame(cashgame);

            Assert.AreEqual(expectedDate, recievedDate);
            Assert.AreEqual(expectedStatus, recievedStatus);
		}

		[Test]
		public void EndGame_CallsUpdateGameAndSetsCurrentDateAndStatusPublished(){
			var id = 1;
			var location = "a";
			var status = GameStatus.Running;
		    var startTime = DateTime.Parse("2001-01-01 01:00:00");

            var cashgame = new Cashgame {Id = id, Location = location, Status = status, StartTime = startTime};

			var expectedStatus = GameStatus.Running;
            var recievedStatus = GameStatus.Created;
            CashgameStorageMock.Setup(o => o.UpdateGame(It.IsAny<RawCashgame>()))
                                .Callback(
                                    (RawCashgame rawCashgame) => {
                                        recievedStatus = rawCashgame.Status;
                                });

            var sut = GetSut();
			sut.StartGame(cashgame);

            Assert.AreEqual(expectedStatus, recievedStatus);
		}

	}

}