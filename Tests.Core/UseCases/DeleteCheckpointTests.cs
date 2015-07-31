using Core.UseCases.DeleteCheckpoint;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class DeleteCheckpointTests : TestBase
    {
        [Test]
        public void DeleteCheckpoint_EndedGame_DeletesCheckpointAndReturnsCorrectReturnUrl()
        {
            var request = new DeleteCheckpointRequest(TestData.SlugA, TestData.DateStringA, TestData.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
            Assert.AreEqual("/bunch-a/cashgame/details/2001-01-01", result.ReturnUrl.Relative);
        }

        [Test]
        public void DeleteCheckpoint_RunningGame_DeletesCheckpointAndReturnsCorrectReturnUrl()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new DeleteCheckpointRequest(TestData.SlugA, TestData.DateStringC, TestData.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
            Assert.AreEqual("/bunch-a/cashgame/running", result.ReturnUrl.Relative);
        }

        private DeleteCheckpointInteractor Sut
        {
            get
            {
                return new DeleteCheckpointInteractor(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Checkpoint);
            }
        }
    }
}