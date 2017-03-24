using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeleteCheckpointTests
{
    public abstract class Arrange : UseCaseTest<DeleteCheckpoint>
    {
        protected DeleteCheckpoint.Result Result;

        private readonly string _checkpointId = GetId(1);
        protected const string BunchId = BunchData.Id1;
        private const string LocationId = "location-id";
        protected const string CashgameId = CashgameData.Id1;
        private const string PlayerId = "player-id";
        protected virtual GameStatus GameStatus => GameStatus.Finished;

        protected Cashgame UpdatedCashgame;

        protected override void Setup()
        {
            UpdatedCashgame = null;

            Mock<ICashgameRepository>().Setup(o => o.GetByCheckpoint(_checkpointId)).Returns(Cashgame);

            Mock<ICashgameRepository>().Setup(o => o.Update(It.IsAny<Cashgame>()))
                .Callback((Cashgame c) => UpdatedCashgame = c);

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(Bunch);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new DeleteCheckpoint.Request(CashgameId, _checkpointId));
        }

        private Bunch Bunch => new Bunch(BunchId);
        private Cashgame Cashgame => new Cashgame(BunchId, LocationId, GameStatus, CashgameId, Checkpoints);
        private IList<Checkpoint> Checkpoints => new List<Checkpoint>
        {
            new BuyinCheckpoint(CashgameId, PlayerId, GetTime(1), 200, 200, GetId(1)),
            new ReportCheckpoint(CashgameId, PlayerId, GetTime(2), 200, 0, GetId(2)),
            new ReportCheckpoint(CashgameId, PlayerId, GetTime(3), 200, 0, GetId(3)),
            new CashoutCheckpoint(CashgameId, PlayerId, GetTime(4), 200, 0, GetId(4))
        };

        private static string GetId(int incr)
        {
            return $"checkpoint-id-{incr}";
        }

        private static DateTime GetTime(int incr)
        {
            return DateTime.Parse($"2001-01-01 10:00:00").AddSeconds(incr);
        }
    }
}