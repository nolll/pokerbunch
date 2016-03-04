using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.CashoutTests
{
    public abstract class Arrange : ArrangeBase
    {
        private const int BunchId = 1;
        private const int CashgameId = 2;
        private const int LocationId = 3;
        private const int UserId = 4;
        protected const int PlayerId = 5;
        protected const string Slug = "slug";
        protected const string UserName = "username";
        protected const int CashoutStack = 123;
        private DateTime _startTime = DateTime.Parse("2001-01-01 12:00:00");
        protected int CheckpointCountBeforeCashout;
        protected virtual bool HasCashedOutBefore => false;
        protected Cashgame UpdatedCashgame;
        protected Cashout Sut;

        [SetUp]
        public void Setup()
        {
            Sut = Mocker.New<Cashout>();

            var cashgame = CreateCashgame();
            CheckpointCountBeforeCashout = cashgame.Checkpoints.Count;
            Mocker.MockOf<IBunchService>().Setup(s => s.GetBySlug(Slug)).Returns(new Bunch(BunchId, Slug));
            Mocker.MockOf<ICashgameService>().Setup(s => s.GetRunning(BunchId)).Returns(CreateCashgame());
            Mocker.MockOf<IPlayerService>().Setup(s => s.GetByUserId(BunchId, UserId)).Returns(new Player(BunchId, PlayerId, UserId));
            Mocker.MockOf<IUserService>().Setup(s => s.GetByNameOrEmail(UserName)).Returns(new User(UserId, UserName));

            Mocker.MockOf<ICashgameService>().Setup(o => o.UpdateGame(It.IsAny<Cashgame>())).Callback((Cashgame c) => UpdatedCashgame = c);
        }

        private Cashgame CreateCashgame()
        {
            if (HasCashedOutBefore)
            {
                var checkpoints1 = new List<Checkpoint>
                {
                    Checkpoint.Create(CashgameId, PlayerId, _startTime, CheckpointType.Buyin, 200, 200, 1),
                    Checkpoint.Create(CashgameId, PlayerId, _startTime.AddMinutes(1), CheckpointType.Cashout, 200, 0, 3)
                };

                return new Cashgame(BunchId, LocationId, GameStatus.Running, CashgameId, checkpoints1);
            }
            else
            {
                var checkpoints1 = new List<Checkpoint>
                {
                    Checkpoint.Create(CashgameId, PlayerId, _startTime, CheckpointType.Buyin, 200, 200, 1)
                };

                return new Cashgame(BunchId, LocationId, GameStatus.Running, CashgameId, checkpoints1);
            }
        }
    }
}