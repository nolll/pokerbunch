using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
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

        protected virtual string BunchId => BunchIdWithoutRunningGame;
        private static readonly DateTime CurrentTime = DateTime.Now;
        protected virtual CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Overview;
        protected virtual int? Year => null;

        protected override void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player, true);

            Mock<ICashgameRepository>().Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);
            Mock<ICashgameRepository>().Setup(o => o.GetYears(BunchId)).Returns(new List<int> { FirstYear, LastYear });
        }

        protected override void Execute()
        {
            var baseContext = new BaseContext.Result("1");
            var coreContext = new CoreContext.Result(baseContext, true, false, UserData.UserName1, UserData.DisplayName1);
            var bunchContext = new BunchContext.Result(coreContext, BunchId, BunchId, BunchData.DisplayName1);
            Result = Subject.Execute(bunchContext, new CashgameContext.Request(BunchId, CurrentTime, SelectedPage, Year));
        }
    }
}