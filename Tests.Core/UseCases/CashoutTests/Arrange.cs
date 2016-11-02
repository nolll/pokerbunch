using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.CashoutTests
{
    public abstract class Arrange : ArrangeBase
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
            Sut = CreateSut<Cashout>();

            var cashgame = CreateCashgame();
            CheckpointCountBeforeCashout = cashgame.Checkpoints.Count;
            MockOf<IBunchRepository>().Setup(s => s.Get(BunchId)).Returns(new Bunch(BunchId, BunchId));
            MockOf<ICashgameRepository>().Setup(s => s.GetRunning(BunchId)).Returns(CreateCashgame());
            MockOf<IPlayerRepository>().Setup(s => s.GetByUser(BunchId, UserId)).Returns(new Player(BunchId, PlayerId, UserId));
            MockOf<IUserRepository>().Setup(s => s.GetByNameOrEmail(UserName)).Returns(new User(UserId, UserName));

            MockOf<ICashgameRepository>().Setup(o => o.Update(It.IsAny<Cashgame>())).Callback((Cashgame c) => UpdatedCashgame = c);
        }

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