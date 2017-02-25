using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public abstract class Arrange : UseCaseTest<CashgameContext>
    {
        protected CashgameContext.Result Result;

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
            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player, true);
            var user = new User(UserData.Id1, UserData.UserName1);

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);
            Mock<ICashgameRepository>().Setup(o => o.GetYears(BunchId)).Returns(new List<int> { FirstYear, LastYear });
            Mock<IUserRepository>().Setup(o => o.GetByNameOrEmail(UserData.UserName1)).Returns(user);

            Result = Sut.Execute(new CashgameContext.Request(UserName, BunchId, CurrentTime, SelectedPage, Year));
        }
    }
}