using System;
using Application.Services;
using Core.Entities;
using Core.Entities.Checkpoints;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.Models.UrlModels;
using Web.Services;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Action
{
    public class CheckpointModelFactoryTests : MockContainer
    {
        private int _stack;
        private DateTime _timestamp;
        private readonly Homegame _homegame;
        private readonly Cashgame _cashgame;
        private readonly Player _player;
        private Checkpoint _checkpoint;
        private Role _role;

        public CheckpointModelFactoryTests()
        {
            _homegame = new FakeHomegame();
            _cashgame = new FakeCashgame(startTime: new DateTime());
            _player = new FakePlayer();
        }

        [SetUp]
        public void SetUp()
        {
            const string description = "description";
            _stack = 200;
            _timestamp = DateTime.Parse("2010-01-01 01:00:00");
            _checkpoint = new FakeCheckpoint(_timestamp, stack: _stack, description: description);
            _role = Role.Player;
        }

        [Test]
        public void Timestamp_IsSet()
        {
            const string formattedTimestamp = "a";
            GetMock<IGlobalization>().Setup(o => o.FormatTime(It.IsAny<DateTime>())).Returns(formattedTimestamp);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.AreEqual(formattedTimestamp, result.Timestamp);
        }

        [Test]
        public void Description_IsSet()
        {
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.AreEqual("description", result.Description);
        }

        [Test]
        public void Stack_IsSet()
        {
            const string formattedStack = "a";
            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<Currency>(), _stack)).Returns(formattedStack);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.AreEqual(formattedStack, result.Stack);
        }

        [Test]
        public void ShowLink_NormalUser_IsFalse()
        {
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.IsFalse(result.ShowLink);
        }

        [Test]
        public void ShowLink_ManagerUser_IsTrue()
        {
            _role = Role.Manager;

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.IsTrue(result.ShowLink);
        }

        [Test]
        public void EditUrl_IsSet()
        {
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _player, _checkpoint, _role);

            Assert.IsInstanceOf<EditCheckpointUrlModel>(result.EditUrl);
        }

        private CheckpointModelFactory GetSut()
        {
            return new CheckpointModelFactory(
                GetMock<IUrlProvider>().Object,
                GetMock<IGlobalization>().Object);
        }
    }
}