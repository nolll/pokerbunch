using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Factories;
using NUnit.Framework;

namespace Infrastructure.Tests.Factories{

	public class CashgameResultFactoryTests {

		private List<Checkpoint> _checkpoints;

        [SetUp]
		public void SetUp(){
		}

        [Test]
		public void GetWinnings_ReturnsDifferenceBetweenLastCheckpointStackAndBuyin(){
			var buyinCheckPoint = new BuyinCheckpoint(new DateTime(), 100, 100);
			var reportCheckpoint = new ReportCheckpoint(new DateTime(), 200);

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
			var checkpoint = new ReportCheckpoint(new DateTime(), 200);
			_checkpoints = new List<Checkpoint> {checkpoint};

			var result = GetResult();

            Assert.AreEqual(1, result.Checkpoints.Count);
		}

		[Test]
		public void GetBuyinTime_WithoutBuyinCheckpoint_ReturnsNull(){
			var otherCheckpoint = new ReportCheckpoint(new DateTime(), 0);
			_checkpoints = new List<Checkpoint> {otherCheckpoint, otherCheckpoint};

		    var result = GetResult();

			Assert.IsNull(result.BuyinTime);
		}

		[Test]
		public void GetBuyinTime_WithBuyinCheckpoint_ReturnsBuyinTime(){
			var buyinTime = DateTime.Parse("2010-01-01 19:00:00");
            var buyinCheckpoint = new BuyinCheckpoint(buyinTime, 0, 200);
			var otherCheckpoint = new ReportCheckpoint(new DateTime(), 0);
		    _checkpoints = new List<Checkpoint> {otherCheckpoint, buyinCheckpoint, otherCheckpoint};

		    var result = GetResult();

            Assert.AreEqual(buyinTime, result.BuyinTime);
		}

		[Test]
		public void GetCashoutTime_WithoutCashoutCheckpoint_ReturnsNull(){
			var otherCheckpoint = new ReportCheckpoint(new DateTime(), 0);
			_checkpoints = new List<Checkpoint> {otherCheckpoint, otherCheckpoint};

			var result = GetResult();

			Assert.IsNull(result.CashoutTime);
		}

		[Test]
		public void GetCashoutTime_WithCashoutCheckpoint_ReturnsCashoutTime(){
			var cashoutTime = DateTime.Parse("2010-01-01 23:00:00");
			var cashoutCheckpoint = new CashoutCheckpoint(cashoutTime, 200);
			var otherCheckpoint = new ReportCheckpoint(new DateTime(), 0);
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
			_checkpoints = new List<Checkpoint> {new CashoutCheckpoint(DateTime.Parse("2010-01-01 02:00:00"), 0)};

			var result = GetResult();

            Assert.AreEqual(0, result.PlayedTime);
		}

		[Test]
		public void GetPlayedTime_MissingEndTime_ReturnsZero(){
			_checkpoints = new List<Checkpoint> {new BuyinCheckpoint(DateTime.Parse("2010-01-01 01:00:00"), 0, 0)};

			var result = GetResult();

            Assert.AreEqual(0, result.PlayedTime);
		}

		[Test]
		public void GetPlayedTime_ReturnsDifferenceBetweenStartDateAndEndDate(){
			_checkpoints = new List<Checkpoint>
			    {
			        new BuyinCheckpoint(DateTime.Parse("2010-01-01 01:00:00"), 0, 0),
                    new CashoutCheckpoint(DateTime.Parse("2010-01-01 02:00:00"), 0)
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
			var player = new Player();
			var factory = new CashgameResultFactory();
			return factory.Create(player, _checkpoints);
		}

	}

}