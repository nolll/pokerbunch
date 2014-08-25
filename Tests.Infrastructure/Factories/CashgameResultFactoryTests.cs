using System;
using System.Collections.Generic;
using Application.Factories;
using Application.Services;
using Core.Entities;
using Core.Entities.Checkpoints;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Factories{

	public class CashgameResultFactoryTests : MockContainer
    {
        private List<Checkpoint> _checkpoints;

        [Test]
		public void GetWinnings_ReturnsDifferenceBetweenLastCheckpointStackAndBuyin(){
			var buyinCheckPoint = new CheckpointInTest(new DateTime(), stack: 100, amount: 100, type: CheckpointType.Buyin);
			var reportCheckpoint = new CheckpointInTest(new DateTime(), stack: 200);

			_checkpoints = new List<Checkpoint> {buyinCheckPoint, reportCheckpoint};

			var result = GetResult();

			Assert.AreEqual(100, result.Winnings);
		}

        [Test]
		public void GetCheckpoints_NoCheckpoints_ReturnsEmptyList(){
			_checkpoints = new List<Checkpoint>();

			var result = GetResult();

            Assert.AreEqual(0, result.Checkpoints.Count);
		}

		[Test]
		public void GetCheckpoints_OneCheckpoint_ReturnsThatCheckpoint(){
			var checkpoint = new CheckpointInTest(new DateTime(), stack: 200);
			_checkpoints = new List<Checkpoint> {checkpoint};

			var result = GetResult();

            Assert.AreEqual(1, result.Checkpoints.Count);
		}

		[Test]
		public void GetBuyinTime_WithoutBuyinCheckpoint_ReturnsNull(){
			var otherCheckpoint = new CheckpointInTest();
			_checkpoints = new List<Checkpoint> {otherCheckpoint, otherCheckpoint};

		    var result = GetResult();

			Assert.IsNull(result.BuyinTime);
		}

		[Test]
		public void GetBuyinTime_WithBuyinCheckpoint_ReturnsBuyinTime(){
			var buyinTime = DateTime.Parse("2010-01-01 19:00:00");
            var buyinCheckpoint = new CheckpointInTest(buyinTime, stack: 0, amount: 200, type: CheckpointType.Buyin);
			var otherCheckpoint = new CheckpointInTest();
		    _checkpoints = new List<Checkpoint> {otherCheckpoint, buyinCheckpoint, otherCheckpoint};

		    var result = GetResult();

            Assert.AreEqual(buyinTime, result.BuyinTime);
		}

		[Test]
		public void GetCashoutTime_WithoutCashoutCheckpoint_ReturnsNull(){
			var otherCheckpoint = new CheckpointInTest();
			_checkpoints = new List<Checkpoint> {otherCheckpoint, otherCheckpoint};

			var result = GetResult();

			Assert.IsNull(result.CashoutTime);
		}

		[Test]
		public void GetCashoutTime_WithCashoutCheckpoint_ReturnsCashoutTime(){
			var cashoutTime = DateTime.Parse("2010-01-01 23:00:00");
            var cashoutCheckpoint = new CheckpointInTest(cashoutTime, stack: 200, type: CheckpointType.Cashout);
			var otherCheckpoint = new CheckpointInTest();
			_checkpoints = new List<Checkpoint> {otherCheckpoint, cashoutCheckpoint, otherCheckpoint};

			var result = GetResult();

            Assert.AreEqual(cashoutTime, result.CashoutTime);
		}

		[Test]
		public void GetPlayedTime_MissingBothStartTimeAndEndTime_ReturnsZero(){
			_checkpoints = new List<Checkpoint>();
			
			var result = GetResult();

            Assert.AreEqual(0, result.PlayedTime);
		}

		[Test]
		public void GetPlayedTime_MissingStartTime_ReturnsZero(){
            _checkpoints = new List<Checkpoint> { new CheckpointInTest(DateTime.Parse("2010-01-01 02:00:00")) };

			var result = GetResult();

            Assert.AreEqual(0, result.PlayedTime);
		}

		[Test]
		public void GetPlayedTime_MissingEndTime_ReturnsZero(){
            _checkpoints = new List<Checkpoint> { new CheckpointInTest(DateTime.Parse("2010-01-01 01:00:00")) };

			var result = GetResult();

            Assert.AreEqual(0, result.PlayedTime);
		}

		[Test]
		public void GetPlayedTime_ReturnsDifferenceBetweenStartDateAndEndDate(){
			_checkpoints = new List<Checkpoint>
			    {
			        new CheckpointInTest(DateTime.Parse("2010-01-01 01:00:00"), CheckpointType.Buyin),
                    new CheckpointInTest(DateTime.Parse("2010-01-01 02:00:00"), CheckpointType.Cashout)
			    };

            var result = GetResult();

            Assert.AreEqual(60, result.PlayedTime);
		}

		[Test]
		public void GetStack_WithoutCheckpoints_ReturnsZero(){
			_checkpoints = new List<Checkpoint>();

			var result = GetResult();

            Assert.AreEqual(0, result.Stack);
		}

		private CashgameResult GetResult(){
			const int playerId = 1;
			var factory = new CashgameResultFactory(GetMock<ITimeProvider>().Object);
			return factory.Create(playerId, _checkpoints);
		}

	}

}