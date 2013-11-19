using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.Models.CashgameModels.Report;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Report{

	public class ReportPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private Player _player;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_player = new FakePlayer();
		}

        private ReportPageModel GetResult(){
			var runningGame = new FakeCashgame();
            return GetSut().Create(new FakeUser(), _homegame, _player, runningGame);
		}

        private ReportPageModelFactory GetSut()
        {
            return new ReportPageModelFactory(Mocks.PagePropertiesFactoryMock.Object);
        }

	}

}