using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.Models.PlayerModels.Facts;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerFactsModelFactory : IPlayerFactsModelFactory
    {
        private readonly IGlobalization _globalization;

        public PlayerFactsModelFactory(IGlobalization globalization)
        {
            _globalization = globalization;
        }

        public PlayerFactsModel Create(CurrencySettings currency, IList<Cashgame> cashgames, Player player)
        {
            var filteredGames = FilterCashgames(cashgames, player);

            return new PlayerFactsModel
                {
                    Winnings = _globalization.FormatResult(currency, GetWinnings(filteredGames, player)),
                    BestResult = _globalization.FormatResult(currency, GetBestResult(filteredGames, player)),
                    WorstResult = _globalization.FormatResult(currency, GetWorstResult(filteredGames, player)),
                    GamesPlayed = filteredGames.Count,
                    TimePlayed = _globalization.FormatDuration(GetMinutesPlayed(filteredGames)),
                    BestResultCount = GetBestResultCount(cashgames, player),
                    WinningStreak = GetWinningStreak(cashgames, player),
                    LosingStreak = GetLosingStreak(cashgames, player)
                };
        }

        private List<Cashgame> FilterCashgames(IEnumerable<Cashgame> cashgames, Player player)
        {
            var filteredCashgames = new List<Cashgame>();
            foreach (var cashgame in cashgames)
            {
                if (cashgame.IsInGame(player.Id))
                {
                    filteredCashgames.Add(cashgame);
                }
            }
            return filteredCashgames;
        }

        private int GetWinnings(IEnumerable<Cashgame> cashgames, Player player)
        {
            var winnings = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(player.Id);
                if (result != null)
                {
                    winnings += result.Winnings;
                }
            }
            return winnings;
        }

        private int GetBestResult(IEnumerable<Cashgame> cashgames, Player player)
        {
            int? best = null;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(player.Id);
                if (!best.HasValue || result != null && result.Winnings > best)
                {
                    best = result.Winnings;
                }
            }
            return best.HasValue ? best.Value : 0;
        }

        private int GetWorstResult(IEnumerable<Cashgame> cashgames, Player player)
        {
            int? worst = null;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(player.Id);
                if (!worst.HasValue || result != null && result.Winnings < worst)
                {
                    worst = result.Winnings;
                }
            }
            return worst.HasValue ? worst.Value : 0;
        }

        private int GetMinutesPlayed(IEnumerable<Cashgame> cashgames)
        {
            var timePlayed = 0;
            foreach (var cashgame in cashgames)
            {
                timePlayed += cashgame.Duration;
            }
            return timePlayed;
        }

        private int GetBestResultCount(IEnumerable<Cashgame> cashgames, Player player)
        {
            var count = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetBestResult();
                if(result.PlayerId == player.Id)
                {
                    count++;
                }
            }
            return count;
        }

        private int GetWinningStreak(IEnumerable<Cashgame> cashgames, Player player)
        {
            var bestStreak = 0;
            var currentStreak = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(player.Id);
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

        private int GetLosingStreak(IEnumerable<Cashgame> cashgames, Player player)
        {
            var worstStreak = 0;
            var currentStreak = 0;
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(player.Id);
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

    }
}