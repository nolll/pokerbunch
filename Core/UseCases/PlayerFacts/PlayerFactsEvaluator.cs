using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.UseCases.PlayerFacts
{
    public class PlayerFactsEvaluator
    {
        private readonly IEnumerable<Cashgame> _cashgames;
        private readonly int _playerId;

        public PlayerFactsEvaluator(IEnumerable<Cashgame> cashgames, int playerId)
        {
            _cashgames = FilterCashgames(cashgames, playerId);
            _playerId = playerId;
        }

        public int GameCount
        {
            get { return _cashgames.Count(); }
        }

        public int MinutesPlayed
        {
            get
            {
                var minutesPlayed = 0;
                foreach (var cashgame in _cashgames)
                {
                    var result = cashgame.GetResult(_playerId);
                    if (result != null)
                    {
                        minutesPlayed += result.PlayedTime;
                    }
                }
                return minutesPlayed;
            }
        }

        public int Winnings
        {
            get
            {
                var winnings = 0;
                foreach (var cashgame in _cashgames)
                {
                    var result = cashgame.GetResult(_playerId);
                    if (result != null)
                    {
                        winnings += result.Winnings;
                    }
                }
                return winnings;
            }
        }

        public int BestResult
        {
            get
            {
                int? best = null;
                foreach (var cashgame in _cashgames)
                {
                    var result = cashgame.GetResult(_playerId);
                    if (!best.HasValue || result != null && result.Winnings > best)
                    {
                        best = result.Winnings;
                    }
                }
                return best.HasValue ? best.Value : 0;
            }
        }

        public int WorstResult
        {
            get
            {
                int? worst = null;
                foreach (var cashgame in _cashgames)
                {
                    var result = cashgame.GetResult(_playerId);
                    if (!worst.HasValue || result != null && result.Winnings < worst)
                    {
                        worst = result.Winnings;
                    }
                }
                return worst.HasValue ? worst.Value : 0;
            }
        }

        public int BestResultCount
        {
            get
            {
                var count = 0;
                foreach (var cashgame in _cashgames)
                {
                    var result = cashgame.GetBestResult();
                    if (result.PlayerId == _playerId)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public int WinningStreak
        {
            get
            {
                var bestStreak = 0;
                var currentStreak = 0;
                foreach (var cashgame in _cashgames)
                {
                    var result = cashgame.GetResult(_playerId);
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
        }

        public int LosingStreak
        {
            get
            {
                var worstStreak = 0;
                var currentStreak = 0;
                foreach (var cashgame in _cashgames)
                {
                    var result = cashgame.GetResult(_playerId);
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

        private static IEnumerable<Cashgame> FilterCashgames(IEnumerable<Cashgame> cashgames, int playerId)
        {
            return cashgames.Where(cashgame => cashgame.IsInGame(playerId)).ToList();
        }
    }
}