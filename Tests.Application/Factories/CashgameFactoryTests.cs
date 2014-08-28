using System;
using System.Collections.Generic;
using Application.Factories;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Factories
{
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
	    private int _bunchId;

        [SetUp]
		public void SetUp()
        {
			_earliestBuyinTime = DateTime.Parse("2010-01-01 01:00:00");
			_earliestCashoutTime = DateTime.Parse("2010-01-01 03:00:00");
			_latestBuyinTime = DateTime.Parse("2010-01-01 02:00:00");
			_latestCashoutTime = DateTime.Parse("2010-01-01 04:00:00");

            _results = new List<CashgameResult>
                {
                    new CashgameResultInTest
                        (
                            buyinTime: _earliestBuyinTime,
                            cashoutTime: _earliestCashoutTime
                        ),
                    new CashgameResultInTest
                        (
                            buyinTime: _latestBuyinTime,
                            cashoutTime: _latestCashoutTime
                        )
                };

			_location = "a";
			_status = GameStatus.Running;
			_id = 1;
            _bunchId = 2;
        }

        [Test]
		public void Get_CashgamePropertiesAreSet()
        {
			var result = CashgameFactory.Create(_location, _bunchId, (int)_status, _id, _results);

			Assert.AreEqual(_location, result.Location);
            Assert.AreEqual(_status, result.Status);
            Assert.AreEqual(_id, result.Id);
            Assert.AreEqual(_bunchId, result.BunchId);
            Assert.AreEqual(_earliestBuyinTime, result.StartTime);
            Assert.AreEqual(_latestCashoutTime, result.EndTime);
            Assert.AreEqual(180, result.Duration);
		}
	}
}