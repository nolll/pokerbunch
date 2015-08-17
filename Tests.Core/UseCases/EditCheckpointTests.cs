using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;
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
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringA, TestData.PlayerIdA, TestData.BuyinCheckpointId, TestData.StartTimeA, -1, ChangedAmount);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCheckpoint_InvalidAmount_ThrowsException()
        {
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringA, TestData.PlayerIdA, TestData.BuyinCheckpointId, TestData.StartTimeA, ChangedStack, -1);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditCheckpoint_ValidInput_ReturnUrlIsSet()
        {
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringA, TestData.PlayerIdA, TestData.BuyinCheckpointId, TestData.StartTimeA, ChangedStack, ChangedAmount);

            var result = Sut.Execute(request);

            Assert.AreEqual("/cashgame/action/bunch-a/2001-01-01/1", result.ReturnUrl.Relative);
        }
        
        [Test]
        public void EditCheckpoint_ValidInput_CheckpointIsSaved()
        {
            var request = new EditCheckpoint.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringA, TestData.PlayerIdA, TestData.BuyinCheckpointId, TestData.StartTimeA, ChangedStack, ChangedAmount);

            Sut.Execute(request);

            Assert.AreEqual(CheckpointType.Buyin, Repos.Checkpoint.Saved.Type);
            Assert.AreEqual(TestData.BuyinCheckpointId, Repos.Checkpoint.Saved.Id);
            Assert.AreEqual(ChangedStack, Repos.Checkpoint.Saved.Stack);
            Assert.AreEqual(ChangedAmount, Repos.Checkpoint.Saved.Amount);
        }

        private EditCheckpoint Sut
        {
            get
            {
                return new EditCheckpoint(
                    Repos.Bunch,
                    Repos.Checkpoint,
                    Repos.User,
                    Repos.Player);
            }
        }
    }
}