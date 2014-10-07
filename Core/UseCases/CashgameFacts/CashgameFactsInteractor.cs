using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases.CashgameFacts
{
    public static class CashgameFactsInteractor
    {
        public static CashgameFactsResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            CashgameFactsRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var players = playerRepository.GetList(bunch).OrderBy(o => o.DisplayName).ToList();
            var cashgames = cashgameRepository.GetPublished(bunch, request.Year);
            var factBuilder = new FactBuilder(cashgames, players);

            return GetFactsResult(playerRepository, bunch, factBuilder);
        }

        // todo: make this method private and test throught the Execute-method
        public static CashgameFactsResult GetFactsResult(IPlayerRepository playerRepository, Bunch bunch, FactBuilder factBuilder)
        {
            var gameCount = factBuilder.GameCount;
            var timePlayed = Time.FromMinutes(factBuilder.TotalGameTime);
            var turnover = new Money(factBuilder.TotalTurnover, bunch.Currency);
            var bestResult = GetBestResult(playerRepository, factBuilder, bunch.Currency);
            var worstResult = GetWorstResult(playerRepository, factBuilder, bunch.Currency);
            var bestTotalResult = GetBestTotalResult(factBuilder, bunch.Currency);
            var worstTotalResult = GetWorstTotalResult(factBuilder, bunch.Currency);
            var mostTimeResult = GetMostTimeResult(factBuilder);
            var biggestTotalBuyin = GetBiggestTotalBuyin(factBuilder, bunch.Currency);
            var biggestTotalCashout = GetBiggestTotalCashout(factBuilder, bunch.Currency);

            return new CashgameFactsResult
            {
                GameCount = gameCount,
                TotalTimePlayed = timePlayed,
                Turnover = turnover,
                BestResult = bestResult,
                WorstResult = worstResult,
                BestTotalResult = bestTotalResult,
                WorstTotalResult = worstTotalResult,
                MostTimePlayed = mostTimeResult,
                BiggestBuyin = biggestTotalBuyin,
                BiggestCashout = biggestTotalCashout
            };
        }

        private static MoneyFact GetBestResult(IPlayerRepository playerRepository, FactBuilder facts, Currency currency)
        {
            var playerName = GetPlayerName(playerRepository, facts.BestResult.PlayerId);
            var amount = new MoneyResult(facts.BestResult.Winnings, currency);
            return new MoneyFact(playerName, amount);
        }

        private static MoneyFact GetWorstResult(IPlayerRepository playerRepository, FactBuilder facts, Currency currency)
        {
            var playerName = GetPlayerName(playerRepository, facts.WorstResult.PlayerId);
            var amount = new MoneyResult(facts.WorstResult.Winnings, currency);
            return new MoneyFact(playerName, amount);
        }

        private static MoneyFact GetBestTotalResult(FactBuilder facts, Currency currency)
        {
            var amount = new MoneyResult(facts.BestTotalResult.Winnings, currency);
            return new MoneyFact(facts.BestTotalResult.Player.DisplayName, amount);
        }

        private static MoneyFact GetWorstTotalResult(FactBuilder facts, Currency currency)
        {
            var amount = new MoneyResult(facts.WorstTotalResult.Winnings, currency);
            return new MoneyFact(facts.WorstTotalResult.Player.DisplayName, amount);
        }

        private static DurationFact GetMostTimeResult(FactBuilder facts)
        {
            var timePlayed = Time.FromMinutes(facts.MostTimeResult.TimePlayed);
            return new DurationFact(facts.MostTimeResult.Player.DisplayName, timePlayed);
        }

        private static MoneyFact GetBiggestTotalBuyin(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.BiggestBuyinTotalResult.Buyin, currency);
            return new MoneyFact(facts.BiggestBuyinTotalResult.Player.DisplayName, amount);
        }

        private static MoneyFact GetBiggestTotalCashout(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.BiggestCashoutTotalResult.Cashout, currency);
            return new MoneyFact(facts.BiggestCashoutTotalResult.Player.DisplayName, amount);
        }

        private static string GetPlayerName(IPlayerRepository playerRepository, int playerId)
        {
            var player = playerRepository.GetById(playerId);
            return player == null ? string.Empty : player.DisplayName;
        }
    }
}