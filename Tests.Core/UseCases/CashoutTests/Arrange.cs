using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashoutTests
{
    public abstract class Arrange : UseCaseTest<Cashout>
    {
        protected Cashout.Result Result;

        private DateTime _startTime = DateTime.Parse("2001-01-01 12:00:00");

        protected virtual int CashoutStack => 123;
        protected DateTime CashoutTime => _startTime.AddMinutes(1);
        protected virtual bool HasCashedOutBefore => false;

        protected int CheckpointCountBeforeCashout;
        protected Cashgame UpdatedCashgame;

        protected override void Setup()
        {
            UpdatedCashgame = null;
            var bunch = new Bunch(BunchData.Id1, BunchData.DisplayName1);
            var cashgame = CreateCashgame();
            CheckpointCountBeforeCashout = cashgame.Checkpoints.Count;
            var player = new Player(BunchData.Id1, PlayerData.Id1, UserData.Id1);
            var user = new User(UserData.Id1, UserData.UserName1);

            Mock<IBunchRepository>().Setup(s => s.Get(BunchData.Id1)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(s => s.GetRunning(BunchData.Id1)).Returns(cashgame);
            Mock<ICashgameRepository>().Setup(o => o.Update(It.IsAny<Cashgame>())).Callback((Cashgame c) => UpdatedCashgame = c);
            Mock<IPlayerRepository>().Setup(s => s.GetByUser(BunchData.Id1, UserData.Id1)).Returns(player);
            Mock<IUserRepository>().Setup(s => s.GetByNameOrEmail(UserData.UserName1)).Returns(user);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new Cashout.Request(UserData.UserName1, BunchData.Id1, PlayerData.Id1, CashoutStack, CashoutTime));
        }

        private Cashgame CreateCashgame()
        {
            if (HasCashedOutBefore)
            {
                var checkpoints1 = new List<Checkpoint>
                {
                    Checkpoint.Create(CashgameData.Id1, PlayerData.Id1, _startTime, CheckpointType.Buyin, 200, 200, "1"),
                    Checkpoint.Create(CashgameData.Id1, PlayerData.Id1, _startTime.AddMinutes(1), CheckpointType.Cashout, 200, 0, "3")
                };

                return new Cashgame(BunchData.Id1, LocationData.Id1, GameStatus.Running, CashgameData.Id1, checkpoints1);
            }
            else
            {
                var checkpoints1 = new List<Checkpoint>
                {
                    Checkpoint.Create(CashgameData.Id1, PlayerData.Id1, _startTime, CheckpointType.Buyin, 200, 200, "1")
                };

                return new Cashgame(BunchData.Id1, LocationData.Id1, GameStatus.Running, CashgameData.Id1, checkpoints1);
            }
        }
    }
}