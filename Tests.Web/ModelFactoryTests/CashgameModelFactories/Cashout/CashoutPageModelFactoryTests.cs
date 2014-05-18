using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Cashout;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Cashout{

    class CashoutPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		
        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
		}
        
        [Test]
		public void CashoutAmount_WithoutPostedAmount_IsNull(){
            var sut = GetSut();
            var result = sut.Create(_homegame, null);

			Assert.IsNull(result.StackAmount);
		}

		[Test]
		public void CashoutAmount_WithPostedAmount_IsSet(){
            var postModel = new CashoutPostModel{ StackAmount = 1 };

            var sut = GetSut();
            var result = sut.Create(_homegame, postModel);

            Assert.AreEqual(1, result.StackAmount);
		}

        private CashoutPageModelFactory GetSut()
        {
            return new CashoutPageModelFactory(GetMock<IPagePropertiesFactory>().Object);
        }

	}

}