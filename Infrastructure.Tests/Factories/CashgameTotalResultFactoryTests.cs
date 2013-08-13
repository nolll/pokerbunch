using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Factories;
using NUnit.Framework;

namespace Infrastructure.Tests.Factories{

	public class CashgameTotalResultFactoryTests {

        [Test]
		public void GetWinnings_WithTwoResults_ReturnsSumOfWinnings(){
			var result = GetResultWithTwoResults();

			Assert.AreEqual(2, result.Winnings);
		}

        private CashgameTotalResult GetResultWithTwoResults()
        {
			var player = new Player();
			var sut = new CashgameTotalResultFactory();
			var cashgameResult = GetResult();
			var totalResults = new List<CashgameResult> {cashgameResult, cashgameResult};
			return sut.Create(player, totalResults);
		}

		private CashgameResult GetResult(){
			var player = new Player();
			return new CashgameResult {Player = player, Winnings = 1};
		}

	}

}