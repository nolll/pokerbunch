using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeleteCheckpointTests
{
    public abstract class Arrange : UseCaseTest<DeleteCheckpoint>
    {
        protected DeleteCheckpoint.Result Result;

        protected const string ActionId = "action-id-1";
        protected const string BunchId = BunchData.Id1;
        protected const string CashgameId = CashgameData.Id1;
        protected string PostedActionId;
        protected virtual bool IsRunning => false;

        protected override void Setup()
        {
            PostedActionId = null;

            var cashgameBunch = new CashgameBunch(BunchId, null, null);
            var cashgame = CashgameData.CreateDetailed(CashgameId, isRunning: IsRunning, bunch: cashgameBunch);
            Mock<ICashgameService>().Setup(o => o.GetDetailedById(CashgameId)).Returns(cashgame);

            Mock<ICashgameService>().Setup(o => o.DeleteAction(ActionId))
                .Callback((string acionId) => PostedActionId = acionId );
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new DeleteCheckpoint.Request(CashgameId, ActionId));
        }
    }
}