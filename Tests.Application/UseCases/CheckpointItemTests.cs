using System;
using Application.Urls;
using Application.UseCases.Actions;
using Core.Entities;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class CheckpointItemTests
    {
        [Test]
        public void Construct_AllPropertiesAreSet()
        {
            const string checkpointType = "a";
            const int stack = 1;
            var time = DateTime.Now;
            var localTime = TimeZoneInfo.ConvertTimeToUtc(time);

            var homegame = new HomegameInTest(timezone: TimeZoneInfo.Utc);
            var cashgame = new CashgameInTest();
            var player = new PlayerInTest();
            const Role role = Role.Player;
            var checkpoint = new CheckpointInTest(description: checkpointType, stack: stack, timestamp: time);

            var result = new CheckpointItem(homegame, cashgame, player, role, checkpoint);

            Assert.AreEqual(checkpointType, result.Type);
            Assert.AreEqual(stack, result.Stack.Amount);
            Assert.AreEqual(localTime, result.Time);
            Assert.IsFalse(result.CanEdit);
            Assert.IsInstanceOf<EditCheckpointUrl>(result.EditUrl);
        }

        [Test]
        public void Construct_WithManager_CanEditIsTrue()
        {
            var homegame = new HomegameInTest(timezone: TimeZoneInfo.Utc);
            var cashgame = new CashgameInTest();
            var player = new PlayerInTest();
            const Role role = Role.Manager;
            var checkpoint = new CheckpointInTest();

            var result = new CheckpointItem(homegame, cashgame, player, role, checkpoint);

            Assert.IsTrue(result.CanEdit);
        }
    }
}
