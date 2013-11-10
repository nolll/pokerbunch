using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.Models.CashgameModels.Cashout;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Cashout{

    class CashoutPageModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		
        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
		}
        
        [Test]
		public void CashoutAmount_WithPostedAmount_IsSetToZero(){
            var runningGame = new FakeCashgame(startTime: new DateTime());
            
            var sut = GetSut();
            var result = sut.Create(new User(), _homegame, runningGame);

			Assert.AreEqual(result.StackAmount, 0);
		}

		[Test]
		public void CashoutAmount_WithPostedAmount_IsSet(){
            var runningGame = new FakeCashgame(startTime: new DateTime());
            var postModel = new CashoutPostModel{ StackAmount = 1 };

            var sut = GetSut();
            var result = sut.Create(new User(), _homegame, runningGame, postModel);

            Assert.AreEqual(1, result.StackAmount);
		}

        private CashoutPageModelFactory GetSut()
        {
            return new CashoutPageModelFactory(Mocks.PagePropertiesFactoryMock.Object);
        }

	}

}