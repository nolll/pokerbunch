using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Report;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories{

	public class ReportPageModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private Player _player;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_player = new Player();
		}

        private ReportPageModel GetResult(){
			var runningGame = new Cashgame();
			return GetSut().Create(new User(), _homegame, _player, runningGame);
		}

        private ReportPageModelFactory GetSut()
        {
            return new ReportPageModelFactory(Mocks.PagePropertiesFactoryMock.Object);
        }

	}

}