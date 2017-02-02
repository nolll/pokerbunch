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

            var bunchRepoMock = new Mock<IBunchRepository>();
            bunchRepoMock.Setup(s => s.Get(BunchId)).Returns(new Bunch(BunchId, BunchId));

            var cashgameRepoMock = new Mock<ICashgameRepository>();
            cashgameRepoMock.Setup(s => s.GetRunning(BunchId)).Returns(CreateCashgame());
            cashgameRepoMock.Setup(o => o.Update(It.IsAny<Cashgame>())).Callback((Cashgame c) => UpdatedCashgame = c);

            var playerRepoMock = new Mock<IPlayerRepository>();
            playerRepoMock.Setup(s => s.GetByUser(BunchId, UserId)).Returns(new Player(BunchId, PlayerId, UserId));

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(s => s.GetByNameOrEmail(UserName)).Returns(new User(UserId, UserName));

            Sut = new Cashout(bunchRepoMock.Object, cashgameRepoMock.Object, playerRepoMock.Object, userRepoMock.Object);
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