using System.Collections.Generic;
using Application.UseCases.PlayerFacts;
using Core.Entities;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class PlayerFactsEvaluatorTests
    {
        [Test]
        public void GameCount_ReturnsOnlyGamesTheSelectedPlayerPlayedIn()
        {
            Assert.AreEqual(4, Sut.GameCount);
        }

        [Test]
        public void MinutesPlayed_ReturnsSumOfMinutesPlayed()
        {
            Assert.AreEqual(8, Sut.MinutesPlayed);
        }

        [Test]
        public void Winnings_ReturnsSumOfWinnings()
        {
            Assert.AreEqual(4, Sut.Winnings);
        }

        [Test]
        public void BestResult_ReturnsBestSingleResult()
        {
            Assert.AreEqual(4, Sut.BestResult);
        }

        [Test]
        public void WorstResult_ReturnsWorstSingleResult()
        {
            Assert.AreEqual(-2, Sut.WorstResult);
        }

        [Test]
        public void BestResultCount_ReturnsNumberOfBestResultOfGame()
        {
            Assert.AreEqual(2, Sut.BestResultCount);
        }

        [Test]
        public void WinningStreak_ReturnsNumberOfPositiveResultsInARow()
        {
            Assert.AreEqual(2, Sut.WinningStreak);
        }

        [Test]
        public void LosingStreak_ReturnsNumberOfNegativeResultsInARow()
        {
            Assert.AreEqual(2, Sut.LosingStreak);
        }

        private PlayerFactsEvaluator Sut
        {
            get
            {
                const int selectedPlayerId = 1;
                const int secondPlayerId = 2;
                const int thirdPlayerId = 3;

                var results1 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(selectedPlayerId, winnings: -1, playedTime: 2),
                        new CashgameResultInTest(secondPlayerId)
                    };
                var cashgame1 = new CashgameInTest(results: results1);

                var results2 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(selectedPlayerId, winnings: -2, playedTime: 2),
                        new CashgameResultInTest(secondPlayerId),
                    };
                var cashgame2 = new CashgameInTest(results: results2);

                var results3 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(selectedPlayerId, winnings: 3, playedTime: 2),
                        new CashgameResultInTest(secondPlayerId),
                    };
                var cashgame3 = new CashgameInTest(results: results3);

                var results4 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(selectedPlayerId, winnings: 4, playedTime: 2),
                        new CashgameResultInTest(secondPlayerId),
                    };
                var cashgame4 = new CashgameInTest(results: results4);

                var results5 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(secondPlayerId),
                        new CashgameResultInTest(thirdPlayerId)
                    };
                var cashgame5 = new CashgameInTest(results: results5);

                var cashgames = new List<Cashgame> { cashgame1, cashgame2, cashgame3, cashgame4, cashgame5 };
                return new PlayerFactsEvaluator(cashgames, selectedPlayerId);
            }
        }
    }
}