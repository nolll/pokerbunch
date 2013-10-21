using System;
using Core.Classes;
using Core.Classes.Checkpoints;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Action;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Action{
	
	public class CheckpointModelFactoryTests : WebMockContainer
	{
	    private readonly Homegame _homegame;
        private readonly Cashgame _cashgame;
	    private readonly Player _player;
		private Checkpoint _checkpoint;
		private Role _role;

	    public CheckpointModelFactoryTests()
	    {
            _homegame = new Homegame();
            _cashgame = new Cashgame { StartTime = new DateTime() };
            _player = new Player();
	    }

        [SetUp]
		public void SetUp(){
            _checkpoint = new Checkpoint{Timestamp = DateTime.Parse("2010-01-01 01:00:00"), Stack = 200};
			_role = Role.Player;
		}

        [Test]
		public void Timestamp_IsSet(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.AreEqual("01:00", result.Timestamp);
		}

		[Test]
		public void Description_IsSet(){
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.AreEqual("Report", result.Description);
		}

		[Test]
		public void Stack_IsSet(){
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

			Assert.AreEqual("$200", result.Stack);
		}

		[Test]
		public void ShowLink_NormalUser_IsFalse(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.IsFalse(result.ShowLink);
		}

		[Test]
		public void ShowLink_ManagerUser_IsTrue(){
			_role = Role.Manager;

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.IsTrue(result.ShowLink);
		}

		[Test]
		public void DeleteUrl_IsSet()
		{
		    const string deleteUrl = "a";
            Mocks.UrlProviderMock.Setup(o => o.GetCashgameCheckpointDeleteUrl(_homegame, _cashgame, _player, _checkpoint)).Returns(deleteUrl);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.AreEqual(deleteUrl, result.EditUrl);
		}

        private CheckpointModelFactory GetSut()
        {
            return new CheckpointModelFactory(
                Mocks.UrlProviderMock.Object);
		}

	}

}