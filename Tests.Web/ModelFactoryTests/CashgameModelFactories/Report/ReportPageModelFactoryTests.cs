using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Report;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Report{

	public class ReportPageModelFactoryTests : MockContainer {

		private Homegame _homegame;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
		}

        private ReportPageModel GetResult(){
            return GetSut().Create(new FakeUser(), _homegame, null);
		}

        private ReportPageModelFactory GetSut()
        {
            return new ReportPageModelFactory(GetMock<IPagePropertiesFactory>().Object);
        }

	}

}