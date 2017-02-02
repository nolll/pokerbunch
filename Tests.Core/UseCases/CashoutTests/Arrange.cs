using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashoutTests
{
    public abstract class Arrange
    {
        protected const string BunchId = "slug";
        private const string CashgameId = "2";
        private const string LocationId = "3";
        private const string UserId = "4";
        protected const string PlayerId = "5";
        protected const string UserName = "username";
        private DateTime _startTime = DateTime.Parse("2001-01-01 12:00:00");

        protected virtual int CashoutStack => 123;
        protected DateTime CashoutTime => _startTime.AddMinutes(1);
        protected virtual bool HasCashedOutBefore => false;

        protected int CheckpointCountBeforeCashout;
        protected Cashgame UpdatedCashgame;
        protected Cashout Sut;

        [SetUp]
        public void Setup()
        {
            var cashgame = CreateCashgame();
            CheckpointCountBeforeCashout = cashgame.Checkpoints.Count;

            var brm = new Mock<IBunchRepository>();
            brm.Setup(s => s.Get(BunchId)).Returns(new Bunch(BunchId, BunchId));

            var crm = new Mock<ICashgameRepository>();
            crm.Setup(s => s.GetRunning(BunchId)).Returns(CreateCashgame());
            crm.Setup(o => o.Update(It.IsAny<Cashgame>())).Callback((Cashgame c) => UpdatedCashgame = c);

            var prm = new Mock<IPlayerRepository>();
            prm.Setup(s => s.GetByUser(BunchId, UserId)).Returns(new Player(BunchId, PlayerId, UserId));

            var urm = new Mock<IUserRepository>();
            urm.Setup(s => s.GetByNameOrEmail(UserName)).Returns(new User(UserId, UserName));

            Sut = new Cashout(brm.Object, crm.Object, prm.Object, urm.Object);
        }

        protected Cashout.Request Request => new Cashout.Request(UserName, BunchId, PlayerId, CashoutStack, CashoutTime);

        private Cashgame CreateCashgame()
        {
            if (HasCashedOutBefore)
            {
                var checkpoints1 = new List<Checkpoint>
                {
                    Checkpoint.Create(CashgameId, PlayerId, _startTime, CheckpointType.Buyin, 200, 200, "1"),
                    Checkpoint.Create(CashgameId, PlayerId, _startTime.AddMinutes(1), CheckpointType.Cashout, 200, 0, "3")
                };

                return new Cashgame(BunchId, LocationId, GameStatus.Running, CashgameId, checkpoints1);
            }
            else
            {
                var checkpoints1 = new List<Checkpoint>
                {
                    Checkpoint.Create(CashgameId, PlayerId, _startTime, CheckpointType.Buyin, 200, 200, "1")
                };

                return new Cashgame(BunchId, LocationId, GameStatus.Running, CashgameId, checkpoints1);
            }
        }
    }
}