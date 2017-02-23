using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public abstract class Arrange
    {
        protected CashgameContext Sut;

        protected const string BunchIdWithoutRunningGame = BunchData.Id1;
        protected const string BunchIdWithRunningGame = BunchData.Id2;
        protected const int FirstYear = 2001;
        protected const int LastYear = 2002;

        private const string UserName = UserData.UserName1;
        protected virtual string BunchId => BunchIdWithoutRunningGame;
        private static readonly DateTime CurrentTime = DateTime.Now;
        protected virtual CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Overview;
        protected virtual int? Year => null;

        [SetUp]
        public void Setup()
        {
            var bunch = new Bunch(BunchId, null, null, null, null, 100);
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchId)).Returns(bunch);

            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player, true);
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);
            crm.Setup(o => o.GetYears(BunchId)).Returns(new List<int> { FirstYear, LastYear });

            var players = PlayerData.TwoPlayers;
            var player = players.First();
            var prm = new Mock<IPlayerRepository>();
            prm.Setup(o => o.List(BunchId)).Returns(players);
            prm.Setup(o => o.GetByUser(BunchId, UserData.Id1)).Returns(player);

            var user = new User(UserData.Id1, UserData.UserName1);
            var urm = new Mock<IUserRepository>();
            urm.Setup(o => o.GetByNameOrEmail(UserData.UserName1)).Returns(user);

            Sut = new CashgameContext(urm.Object, brm.Object, crm.Object);
        }

        protected CashgameContext.Request Request => new CashgameContext.Request(UserName, BunchId, CurrentTime, SelectedPage, Year);
    }
}