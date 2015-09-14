﻿using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EditCheckpointTests : TestBase
    {
        private const int ChangedStack = 1;
        private const int ChangedAmount = 2;

        [Test]
        public void EditCheckpoint_InvalidStack_ThrowsException()
        {
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.BuyinCheckpointId, TestData.StartTimeA, -1, ChangedAmount);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCheckpoint_InvalidAmount_ThrowsException()
        {
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.BuyinCheckpointId, TestData.StartTimeA, ChangedStack, -1);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCheckpoint_ValidInput_ReturnUrlIsSet()
        {
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.BuyinCheckpointId, TestData.StartTimeA, ChangedStack, ChangedAmount);

            var result = Sut.Execute(request);

            Assert.AreEqual(1, result.CashgameId);
            Assert.AreEqual(1, result.PlayerId);
        }
        
        [Test]
        public void EditCheckpoint_ValidInput_CheckpointIsSaved()
        {
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.BuyinCheckpointId, TestData.StartTimeA, ChangedStack, ChangedAmount);

            Sut.Execute(request);

            Assert.AreEqual(CheckpointType.Buyin, Repos.Cashgame.SavedCheckpoint.Type);
            Assert.AreEqual(TestData.BuyinCheckpointId, Repos.Cashgame.SavedCheckpoint.Id);
            Assert.AreEqual(ChangedStack, Repos.Cashgame.SavedCheckpoint.Stack);
            Assert.AreEqual(ChangedAmount, Repos.Cashgame.SavedCheckpoint.Amount);
        }

        private EditCheckpoint Sut
        {
            get
            {
                return new EditCheckpoint(
                    Services.BunchService,
                    Services.UserService,
                    Services.PlayerService,
                    Services.CashgameService);
            }
        }
    }
}