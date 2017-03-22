using System.Linq;
using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.EditCheckpointTests
{
    public class Arrange : UseCaseTest<EditCheckpoint>
    {
        protected EditCheckpoint.Result Result;

        protected Checkpoint UpdatedCheckpoint; 

        protected const int ChangedStack = 1;
        protected const int ChangedAmount = 2;

        protected override void Setup()
        {
            UpdatedCheckpoint = null;
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.BuyinCheckpointId, TestData.StartTimeA, ChangedStack, ChangedAmount));
        }
    }

    public class EditCheckpointTests : Arrange
    {
        [Test]
        public void CashgameIdIsSet()
        {
            Assert.AreEqual("1", Result.CashgameId);
        }

        [Test]
        public void PlayerIdIsSet()
        {
            Assert.AreEqual("1", Result.PlayerId);
        }

        [Test]
        public void EditCheckpoint_ValidInput_CheckpointIsSaved()
        {
            Assert.AreEqual(CheckpointType.Buyin, UpdatedCheckpoint.Type);
            Assert.AreEqual(TestData.BuyinCheckpointId, UpdatedCheckpoint.Id);
            Assert.AreEqual(ChangedStack, UpdatedCheckpoint.Stack);
            Assert.AreEqual(ChangedAmount, UpdatedCheckpoint.Amount);
        }
    }
}