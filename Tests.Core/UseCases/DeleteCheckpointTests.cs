using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class DeleteCheckpointTests : TestBase
    {
        [Test]
        public void DeleteCheckpoint_EndedGame_DeletesCheckpointAndReturnsCorrectReturnUrl()
        {
            var request = new DeleteCheckpoint.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringA, TestData.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
            Assert.AreEqual("/cashgame/details/1", result.ReturnUrl.Relative);
        }

        [Test]
        public void DeleteCheckpoint_RunningGame_DeletesCheckpointAndReturnsCorrectReturnUrl()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new DeleteCheckpoint.Request(TestData.ManagerUser.UserName, TestData.SlugA, TestData.DateStringC, TestData.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
            Assert.AreEqual("/cashgame/running/bunch-a", result.ReturnUrl.Relative);
        }

        private DeleteCheckpoint Sut
        {
            get
            {
                return new DeleteCheckpoint(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Checkpoint,
                    Repos.User,
                    Repos.Player);
            }
        }
    }
}