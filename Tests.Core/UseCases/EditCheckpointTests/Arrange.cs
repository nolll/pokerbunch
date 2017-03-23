using System;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCheckpointTests
{
    public class Arrange : UseCaseTest<EditCheckpoint>
    {
        protected EditCheckpoint.Result Result;

        protected const string CashgameId = CashgameData.Id1;
        protected const string PlayerId = PlayerData.Id1;
        protected const string ActionId = "1";
        protected DateTime Timestamp = DateTime.Parse("2010-10-10 10:10:10");
        protected const int Stack = 1111;
        protected const int Amount = 2222;

        protected string PostedActionId;
        protected DateTime PostedTimestamp;
        protected int PostedStack;
        protected int PostedAmount;

        protected override void Setup()
        {
            PostedActionId = null;
            PostedTimestamp = DateTime.MinValue;
            PostedStack = 0;
            PostedAmount = 0;
        
            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(CashgameId)).Returns(CashgameData.GameWithTwoPlayers(Role.Admin));

            Mock<ICashgameRepository>()
                .Setup(o => o.UpdateAction("1", It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback((string actionId, DateTime timestamp, int stack, int added) =>
                {
                    PostedActionId = actionId;
                    PostedTimestamp = timestamp;
                    PostedStack = stack;
                    PostedAmount = added;
                });
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EditCheckpoint.Request(CashgameId, ActionId, Timestamp, Stack, Amount));
        }
    }
}