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
            var request = new DeleteCheckpointRequest(Constants.SlugA, Constants.DateStringA, Constants.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(Constants.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
            Assert.AreEqual("/bunch-a/cashgame/details/2001-01-01", result.ReturnUrl.Relative);
        }

        [Test]
        public void DeleteCheckpoint_RunningGame_DeletesCheckpointAndReturnsCorrectReturnUrl()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new DeleteCheckpointRequest(Constants.SlugA, Constants.DateStringC, Constants.ReportCheckpointId);
            var result = Sut.Execute(request);

            Assert.AreEqual(Constants.ReportCheckpointId, Repos.Checkpoint.Deleted.Id);
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