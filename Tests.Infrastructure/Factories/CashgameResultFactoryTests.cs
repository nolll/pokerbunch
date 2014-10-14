using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Infrastructure.Factories{

	public class CashgameResultFactoryTests : TestBase
    {
        private List<Checkpoint> _checkpoints;

        [Test]
		public void GetWinnings_ReturnsDifferenceBetweenLastCheckpointStackAndBuyin(){
			var buyinCheckPoint = A.Checkpoint.WithTimestamp(new DateTime()).WithStack(100).WithAmount(100).OfType(CheckpointType.Buyin).Build();
            var reportCheckpoint = A.Checkpoint.WithTimestamp(new DateTime()).WithStack(200).Build();

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
            var checkpoint = A.Checkpoint.WithTimestamp(new DateTime()).WithStack(200).Build();
			_checkpoints = new List<Checkpoint> {checkpoint};

			var result = GetResult();

            Assert.AreEqual(1, result.Checkpoints.Count);
		}

		[Test]
		public void GetBuyinTime_WithoutBuyinCheckpoint_ReturnsNull(){
            var otherCheckpoint = A.Checkpoint.Build();
			_checkpoints = new List<Checkpoint> {otherCheckpoint, otherCheckpoint};

		    var result = GetResult();

			Assert.IsNull(result.BuyinTime);
		}

		[Test]
		public void GetBuyinTime_WithBuyinCheckpoint_ReturnsBuyinTime(){
			var buyinTime = DateTime.Parse("2010-01-01 19:00:00");
            var buyinCheckpoint = A.Checkpoint.WithTimestamp(buyinTime).WithAmount(200).OfType(CheckpointType.Buyin).Build();
            var otherCheckpoint = A.Checkpoint.Build();
		    _checkpoints = new List<Checkpoint> {otherCheckpoint, buyinCheckpoint, otherCheckpoint};

		    var result = GetResult();

            Assert.AreEqual(buyinTime, result.BuyinTime);
		}

		[Test]
		public void GetCashoutTime_WithoutCashoutCheckpoint_ReturnsNull(){
            var otherCheckpoint = A.Checkpoint.Build();
			_checkpoints = new List<Checkpoint> {otherCheckpoint, otherCheckpoint};

			var result = GetResult();

			Assert.IsNull(result.CashoutTime);
		}

		[Test]
		public void GetCashoutTime_WithCashoutCheckpoint_ReturnsCashoutTime(){
			var cashoutTime = DateTime.Parse("2010-01-01 23:00:00");
		    var cashoutCheckpoint = A.Checkpoint.WithTimestamp(cashoutTime).WithStack(200).OfType(CheckpointType.Cashout).Build();
            var otherCheckpoint = A.Checkpoint.Build();
			_checkpoints = new List<Checkpoint> {otherCheckpoint, otherCheckpoint, cashoutCheckpoint};

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
            _checkpoints = new List<Checkpoint> { A.Checkpoint.WithTimestamp(DateTime.Parse("2010-01-01 01:00:00")).Build() };

			var result = GetResult();

            Assert.AreEqual(0, result.PlayedTime);
		}

		[Test]
		public void GetPlayedTime_MissingEndTime_ReturnsZero(){
            _checkpoints = new List<Checkpoint> { A.Checkpoint.WithTimestamp(DateTime.Parse("2010-01-01 02:00:00")).Build() };

			var result = GetResult();

            Assert.AreEqual(0, result.PlayedTime);
		}

		[Test]
		public void GetPlayedTime_ReturnsDifferenceBetweenStartDateAndEndDate(){
			_checkpoints = new List<Checkpoint>
			    {
                    A.Checkpoint.WithTimestamp(DateTime.Parse("2010-01-01 01:00:00")).OfType(CheckpointType.Buyin).Build(),
                    A.Checkpoint.WithTimestamp(DateTime.Parse("2010-01-01 02:00:00")).OfType(CheckpointType.Cashout).Build(),
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
			return new CashgameResult(playerId, _checkpoints);
		}

	}

}