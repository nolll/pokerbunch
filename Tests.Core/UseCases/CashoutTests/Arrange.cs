using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashoutTests
{
    public abstract class Arrange : UseCaseTest<PlayerList>
    {
        private DateTime _startTime = DateTime.Parse("2001-01-01 12:00:00");

        protected virtual int CashoutStack => 123;
        protected DateTime CashoutTime => _startTime.AddMinutes(1);
        protected virtual bool HasCashedOutBefore => false;

        protected int CheckpointCountBeforeCashout;
        protected Cashgame UpdatedCashgame;

        [SetUp]
        public void Setup()
        {
            var cashgame = CreateCashgame();
            CheckpointCountBeforeCashout = cashgame.Checkpoints.Count;

            var brm = new Mock<IBunchRepository>();
            brm.Setup(s => s.Get(BunchData.Id1)).Returns(new Bunch(BunchData.Id1, BunchData.DisplayName1));

            var crm = new Mock<ICashgameRepository>();
            crm.Setup(s => s.GetRunning(BunchData.Id1)).Returns(CreateCashgame());
            crm.Setup(o => o.Update(It.IsAny<Cashgame>())).Callback((Cashgame c) => UpdatedCashgame = c);

            var prm = new Mock<IPlayerRepository>();
            prm.Setup(s => s.GetByUser(BunchData.Id1, UserData.Id1)).Returns(new Player(BunchData.Id1, PlayerData.Id1, UserData.Id1));

            var urm = new Mock<IUserRepository>();
            urm.Setup(s => s.GetByNameOrEmail(UserData.UserName1)).Returns(new User(UserData.Id1, UserData.UserName1));

            Sut = new Cashout(brm.Object, crm.Object, prm.Object, urm.Object);
        }

        protected Cashout.Request Request => new Cashout.Request(UserData.UserName1, BunchData.Id1, PlayerData.Id1, CashoutStack, CashoutTime);

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