using System;
using Core.Classes;
using Core.Classes.Checkpoints;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Action;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Action{
	
	public class CheckpointModelTests : WebMockContainer {

		private Checkpoint _checkpoint;
		private Role _role;

        [SetUp]
		public void SetUp(){
			_checkpoint = new Checkpoint{Timestamp = DateTime.Parse("2010-01-01 01:00:00"), Stack = 200};
			_role = Role.Player;
		}

        [Test]
		public void Timestamp_IsSet(){
			var sut = GetSut();

			Assert.AreEqual(sut.Timestamp, "01:00");
		}

		[Test]
		public void Description_IsSet(){
			var sut = GetSut();

			Assert.AreEqual(sut.Description, "Report");
		}

		[Test]
		public void Stack_IsSet(){
			var sut = GetSut();

			Assert.AreEqual(sut.Stack, "$200");
		}

		[Test]
		public void ShowLink_NormalUser_IsFalse(){
			var sut = GetSut();

			Assert.IsFalse(sut.ShowLink);
		}

		[Test]
		public void ShowLink_ManagerUser_IsTrue(){
			_role = Role.Manager;

			var sut = GetSut();

			Assert.IsTrue(sut.ShowLink);
		}

		[Test]
		public void EditUrl_IsCorrectType(){
			var sut = GetSut();

            Assert.IsInstanceOf<CashgameCheckpointDeleteUrlModel>(sut.EditUrl);
		}

        private CheckpointModel GetSut(){
			var cashgame = new Cashgame {StartTime = new DateTime()};
            var player = new Player();
			return new CheckpointModel(new Homegame(), cashgame, player, _checkpoint, _role);
		}

	}

}