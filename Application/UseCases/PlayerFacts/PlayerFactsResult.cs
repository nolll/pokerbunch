using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Application.UseCases.PlayerFacts
{
    public class PlayerFactsResult
    {
        public MoneyResult Winnings { get; private set; }
        public MoneyResult BestResult { get; private set; }
        public MoneyResult WorstResult { get; private set; }
        public int GamesPlayed { get; private set; }
        public Time TimePlayed { get; private set; }
        public int BestResultCount { get; private set; }
        public int WinningStreak { get; private set; }
        public int LosingStreak { get; private set; }

        public PlayerFactsResult(IList<Cashgame> cashgames, int playerId, Currency currency)
        {
            var filteredCashgames = FilterCashgames(cashgames, playerId);

            Winnings = new MoneyResult(GetWinnings(filteredCashgames, playerId), currency);
            BestResult = new MoneyResult(GetBestResult(filteredCashgames, playerId), currency);
            WorstResult = new MoneyResult(GetWorstResult(filteredCashgames, playerId), currency);
            GamesPlayed = filteredCashgames.Count;
            TimePlayed = Time.FromMinutes(GetMinutesPlayed(filteredCashgames));
            BestResultCount = GetBestResultCount(cashgames, playerId);
            WinningStreak = GetWinningStreak(cashgames, playerId);
            LosingStreak = GetLosingStreak(cashgames, playerId);
        }

        private int GetWinnings(IEnumerable<Cashgame> cashgames, int playerId)
        {
            var winnings = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(playerId);
                if (result != null)
                {
                    winnings += result.Winnings;
                }
            }
            return winnings;
        }

        private int GetBestResult(IEnumerable<Cashgame> cashgames, int playerId)
        {
            int? best = null;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(playerId);
                if (!best.HasValue || result != null && result.Winnings > best)
                {
                    best = result.Winnings;
                }
            }
            return best.HasValue ? best.Value : 0;
        }

        private int GetWorstResult(IEnumerable<Cashgame> cashgames, int playerId)
        {
            int? worst = null;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(playerId);
                if (!worst.HasValue || result != null && result.Winnings < worst)
                {
                    worst = result.Winnings;
                }
            }
            return worst.HasValue ? worst.Value : 0;
        }

        private int GetMinutesPlayed(IEnumerable<Cashgame> cashgames)
        {
            return cashgames.Sum(cashgame => cashgame.Duration);
        }

        private int GetBestResultCount(IEnumerable<Cashgame> cashgames, int playerId)
        {
            var count = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetBestResult();
                if (result.PlayerId == playerId)
                {
                    count++;
                }
            }
            return count;
        }

        private int GetWinningStreak(IEnumerable<Cashgame> cashgames, int playerId)
        {
            var bestStreak = 0;
            var currentStreak = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(playerId);
                if (result == null)
                {
                    continue;
                }
                if (result.Winnings > 0)
                {
                    currentStreak++;
                    if (currentStreak > bestStreak)
                    {
                        bestStreak = currentStreak;
                    }
                }
                else
                {
                    currentStreak = 0;
                }
            }
            return bestStreak;
        }

        private int GetLosingStreak(IEnumerable<Cashgame> cashgames, int playerId)
        {
            var worstStreak = 0;
            var currentStreak = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(playerId);
                if (result == null)
                {
                    continue;
                }
                if (result.Winnings < 0)
                {
                    currentStreak++;
                    if (currentStreak > worstStreak)
                    {
                        worstStreak = currentStreak;
                    }
                }
                else
                {
                    currentStreak = 0;
                }
            }
            return worstStreak;
        }

        private List<Cashgame> FilterCashgames(IEnumerable<Cashgame> cashgames, int playerId)
        {
            var filteredCashgames = new List<Cashgame>();
            foreach (var cashgame in cashgames)
            {
                if (cashgame.IsInGame(playerId))
                {
                    filteredCashgames.Add(cashgame);
                }
            }
            return filteredCashgames;
        }
    }
}