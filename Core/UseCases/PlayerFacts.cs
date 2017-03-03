using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class PlayerFacts
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerFacts(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.Get(request.PlayerId);
            var bunch = _bunchRepository.Get(player.BunchId);
            var cashgames = _cashgameRepository.PlayerList(request.PlayerId);

            return new Result(cashgames, request.PlayerId, bunch.Currency);
        }

        public class Request
        {
            public string PlayerId { get; }

            public Request(string playerId)
            {
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public Money Winnings { get; private set; }
            public Money BestResult { get; private set; }
            public Money WorstResult { get; private set; }
            public int GamesPlayed { get; private set; }
            public Time TimePlayed { get; private set; }
            public int BestResultCount { get; private set; }
            public int CurrentStreak { get; private set; }
            public int WinningStreak { get; private set; }
            public int LosingStreak { get; private set; }

            public Result(IEnumerable<ListCashgame> cashgames, string playerId, Currency currency)
            {
                var evaluator = new PlayerFactsEvaluator(cashgames, playerId);

                Winnings = new Money(evaluator.Winnings, currency);
                BestResult = new Money(evaluator.BestResult, currency);
                WorstResult = new Money(evaluator.WorstResult, currency);
                GamesPlayed = evaluator.GameCount;
                TimePlayed = Time.FromMinutes(evaluator.MinutesPlayed);
                BestResultCount = evaluator.BestResultCount;
                CurrentStreak = evaluator.CurrentStreak;
                WinningStreak = evaluator.WinningStreak;
                LosingStreak = evaluator.LosingStreak;
            }
        }

        private class PlayerFactsEvaluator
        {
            private readonly IEnumerable<ListCashgame> _cashgames;
            private readonly string _playerId;

            public PlayerFactsEvaluator(IEnumerable<ListCashgame> cashgames, string playerId)
            {
                _cashgames = cashgames;
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
                            minutesPlayed += result.PlayedMinutes;
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
                    return best ?? 0;
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
                    return worst ?? 0;
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
                        if (result.Id == _playerId)
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }

            public int CurrentStreak
            {
                get
                {
                    var lastStreak = 0;
                    var currentStreak = 0;
                    var cashgames = _cashgames.Reverse();
                    foreach (var cashgame in cashgames)
                    {
                        var result = cashgame.GetResult(_playerId);
                        if (result.Winnings >= 0)
                        {
                            currentStreak++;
                        }
                        else
                        {
                            currentStreak--;
                        }
                        if (Math.Abs(currentStreak) < Math.Abs(lastStreak))
                        {
                            return lastStreak;
                        }
                        lastStreak = currentStreak;
                    }
                    return lastStreak;
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
                        if (result.Winnings >= 0)
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
        }
    }
}