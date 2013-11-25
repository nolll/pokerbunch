using System.Collections.Generic;
using Core.Classes;
using Core.Factories;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Factories{

	public class CashgameTotalResultFactoryTests {

        [Test]
		public void GetWinnings_WithTwoResults_ReturnsSumOfWinnings(){
			var result = GetResultWithTwoResults();

			Assert.AreEqual(2, result.Winnings);
		}

        private CashgameTotalResult GetResultWithTwoResults()
        {
			var player = new FakePlayer();
			var sut = new CashgameTotalResultFactory();
			var cashgameResult = GetResult();
			var totalResults = new List<CashgameResult> {cashgameResult, cashgameResult};
			return sut.Create(player, totalResults);
		}

		private CashgameResult GetResult(){
			var player = new FakePlayer();
			return new FakeCashgameResult(player, winnings: 1);
		}

	}

}