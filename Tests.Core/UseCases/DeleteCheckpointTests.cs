using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class DeleteCheckpointTests : TestBase
    {
        [Test]
        public void DeleteCheckpoint_EndedGame_DeletesCheckpointAndReturnsCorrectValues()
        {
            var request = new DeleteCheckpoint.Request(TestData.ManagerUser.UserName, TestData.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
            Assert.AreEqual("bunch-a", result.Slug);
            Assert.AreEqual(1, result.CashgameId);
            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void DeleteCheckpoint_RunningGame_DeletesCheckpointAndReturnsCorrectValues()
        {
            Repos.Cashgame.SetupRunningGame();
            Repos.Checkpoint.SetupRunningGame();

            var request = new DeleteCheckpoint.Request(TestData.ManagerUser.UserName, TestData.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
            Assert.AreEqual("bunch-a", result.Slug);
            Assert.AreEqual(3, result.CashgameId);
            Assert.IsTrue(result.GameIsRunning);
        }

        private DeleteCheckpoint Sut
        {
            get
            {
                return new DeleteCheckpoint(
                    Services.BunchService,
                    Services.CashgameService,
                    Repos.Checkpoint,
                    Services.UserService,
                    Services.PlayerService);
            }
        }
    }
}