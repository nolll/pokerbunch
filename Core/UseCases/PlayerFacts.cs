﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerFacts
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserService _userService;

        public PlayerFacts(BunchService bunchService, CashgameService cashgameService, IPlayerRepository playerRepository, UserService userService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerRepository = playerRepository;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            RoleHandler.RequirePlayer(user, player);
            var bunch = _bunchService.Get(player.BunchId);
            var cashgames = _cashgameService.GetFinished(bunch.Id);

            return new Result(cashgames, player.Id, bunch.Currency);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string userName, int playerId)
            {
                UserName = userName;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public MoneyResult Winnings { get; private set; }
            public MoneyResult BestResult { get; private set; }
            public MoneyResult WorstResult { get; private set; }
            public int GamesPlayed { get; private set; }
            public Time TimePlayed { get; private set; }
            public int BestResultCount { get; private set; }
            public int WinningStreak { get; private set; }
            public int LosingStreak { get; private set; }

            public Result(IEnumerable<Cashgame> cashgames, int playerId, Currency currency)
            {
                var evaluator = new PlayerFactsEvaluator(cashgames, playerId);

                Winnings = new MoneyResult(evaluator.Winnings, currency);
                BestResult = new MoneyResult(evaluator.BestResult, currency);
                WorstResult = new MoneyResult(evaluator.WorstResult, currency);
                GamesPlayed = evaluator.GameCount;
                TimePlayed = Time.FromMinutes(evaluator.MinutesPlayed);
                BestResultCount = evaluator.BestResultCount;
                WinningStreak = evaluator.WinningStreak;
                LosingStreak = evaluator.LosingStreak;
            }
        }

        private class PlayerFactsEvaluator
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
}