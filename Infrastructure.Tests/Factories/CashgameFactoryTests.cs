using System;
using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Factories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Factories{

	public class CashgameFactoryTests : MockContainer
    {
        private DateTime _earliestBuyinTime;
		private DateTime _earliestCashoutTime;
		private DateTime _latestBuyinTime;
		private DateTime _latestCashoutTime;

		private List<CashgameResult> _results;
		private string _location;
		private GameStatus _status;
		private int _id;

        [SetUp]
		public void SetUp(){
			_earliestBuyinTime = DateTime.Parse("2010-01-01 01:00:00");
			_earliestCashoutTime = DateTime.Parse("2010-01-01 03:00:00");
			_latestBuyinTime = DateTime.Parse("2010-01-01 02:00:00");
			_latestCashoutTime = DateTime.Parse("2010-01-01 04:00:00");

            _results = new List<CashgameResult>
                {
                    new FakeCashgameResult
                        (
                            buyinTime: _earliestBuyinTime,
                            cashoutTime: _earliestCashoutTime
                        ),
                    new FakeCashgameResult
                        (
                            buyinTime: _latestBuyinTime,
                            cashoutTime: _latestCashoutTime
                        )
                };

			_location = "a";
			_status = GameStatus.Running;
			_id = 1;
		}

        [Test]
		public void Get_CashgamePropertiesAreSet(){
			var sut = GetSut();
			var result = sut.Create(_location, (int)_status, _id, _results);

			Assert.AreEqual(_location, result.Location);
            Assert.AreEqual(_status, result.Status);
            Assert.AreEqual(_id, result.Id);
            Assert.AreEqual(_earliestBuyinTime, result.StartTime);
            Assert.AreEqual(_latestCashoutTime, result.EndTime);
            Assert.AreEqual(180, result.Duration);
		}

		private CashgameFactory GetSut(){
			return new CashgameFactory(
                GetMock<ICashgameResultFactory>().Object,
                GetMock<ICheckpointFactory>().Object);
		}

	}

}