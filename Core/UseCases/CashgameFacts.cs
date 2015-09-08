﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameFacts
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserService _userService;

        public CashgameFacts(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, UserService userService)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var players = _playerRepository.GetList(bunch.Id).OrderBy(o => o.DisplayName).ToList();
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, request.Year);
            var factBuilder = new FactBuilder(cashgames, players);

            return GetFactsResult(_playerRepository, bunch, factBuilder);
        }

        private static Result GetFactsResult(IPlayerRepository playerRepository, Bunch bunch, FactBuilder factBuilder)
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

            return new Result(gameCount, timePlayed, turnover, bestResult, worstResult, bestTotalResult, worstTotalResult, mostTimeResult, biggestTotalBuyin, biggestTotalCashout);
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

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public int? Year { get; private set; }

            public Request(string userName, string slug, int? year)
            {
                UserName = userName;
                Slug = slug;
                Year = year;
            }
        }

        public class Result
        {
            public int GameCount { get; private set; }
            public Time TotalTimePlayed { get; private set; }
            public Money Turnover { get; private set; }
            public MoneyFact BestResult { get; private set; }
            public MoneyFact WorstResult { get; private set; }
            public MoneyFact BestTotalResult { get; private set; }
            public MoneyFact WorstTotalResult { get; private set; }
            public DurationFact MostTimePlayed { get; private set; }
            public MoneyFact BiggestBuyin { get; private set; }
            public MoneyFact BiggestCashout { get; private set; }

            public Result(int gameCount, Time totalTimePlayed, Money turnover, MoneyFact bestResult, MoneyFact worstResult, MoneyFact bestTotalResult, MoneyFact worstTotalResult, DurationFact mostTimePlayed, MoneyFact biggestBuyin, MoneyFact biggestCashout)
            {
                GameCount = gameCount;
                TotalTimePlayed = totalTimePlayed;
                Turnover = turnover;
                BestResult = bestResult;
                WorstResult = worstResult;
                BestTotalResult = bestTotalResult;
                WorstTotalResult = worstTotalResult;
                MostTimePlayed = mostTimePlayed;
                BiggestBuyin = biggestBuyin;
                BiggestCashout = biggestCashout;
            }
        }

        public class DurationFact : PlayerFact
        {
            public Time Time { get; private set; }

            public DurationFact(string playerName, Time time)
                : base(playerName)
            {
                Time = time;
            }
        }

        public class MoneyFact : PlayerFact
        {
            public Money Amount { get; private set; }

            public MoneyFact(string playerName, Money amount)
                : base(playerName)
            {
                Amount = amount;
            }
        }

        public class PlayerFact
        {
            public string PlayerName { get; private set; }

            protected PlayerFact(string playerName)
            {
                PlayerName = playerName;
            }
        }

        private class FactBuilder
        {
            public int GameCount { get; private set; }
            public CashgameResult BestResult { get; private set; }
            public CashgameResult WorstResult { get; private set; }
            public CashgameTotalResult BestTotalResult { get; private set; }
            public CashgameTotalResult WorstTotalResult { get; private set; }
            public CashgameTotalResult MostTimeResult { get; private set; }
            public CashgameTotalResult BiggestBuyinTotalResult { get; private set; }
            public CashgameTotalResult BiggestCashoutTotalResult { get; private set; }
            public int TotalGameTime { get; private set; }
            public int TotalTurnover { get; private set; }

            public FactBuilder(IList<Cashgame> cashgames, IEnumerable<Player> players)
            {
                var gameData = GetGameData(cashgames);
                var totalResults = GetTotalResults(players, cashgames);

                GameCount = gameData.GameCount;
                BestResult = gameData.BestResult;
                WorstResult = gameData.WorstResult;
                TotalGameTime = gameData.TotalGameTime;
                TotalTurnover = gameData.TotalTurnover;
                MostTimeResult = GetMostTimeResult(totalResults);
                BiggestBuyinTotalResult = GetBiggestTotalBuyinResult(totalResults);
                BiggestCashoutTotalResult = GetBiggestTotalCashoutResult(totalResults);
                BestTotalResult = totalResults.FirstOrDefault();
                WorstTotalResult = totalResults.LastOrDefault();
            }

            private GameData GetGameData(IList<Cashgame> cashgames)
            {
                CashgameResult bestResult = null;
                CashgameResult worstResult = null;
                var totalGameTime = 0;
                var totalTurnover = 0;

                foreach (var cashgame in cashgames)
                {
                    var results = cashgame.Results;
                    foreach (var result in results)
                    {
                        if (bestResult == null || result.Winnings > bestResult.Winnings)
                        {
                            bestResult = result;
                        }
                        if (worstResult == null || result.Winnings < worstResult.Winnings)
                        {
                            worstResult = result;
                        }
                    }
                    totalGameTime += cashgame.Duration;
                    totalTurnover += cashgame.Turnover;
                }

                return new GameData(cashgames.Count(), bestResult, worstResult, totalGameTime, totalTurnover);
            }

            private class GameData
            {
                public int GameCount { get; private set; }
                public CashgameResult BestResult { get; private set; }
                public CashgameResult WorstResult { get; private set; }
                public int TotalGameTime { get; private set; }
                public int TotalTurnover { get; private set; }

                public GameData(int gameCount, CashgameResult bestResult, CashgameResult worstResult, int totalGameTime, int totalTurnover)
                {
                    GameCount = gameCount;
                    BestResult = bestResult;
                    WorstResult = worstResult;
                    TotalGameTime = totalGameTime;
                    TotalTurnover = totalTurnover;
                }
            }

            private IList<CashgameTotalResult> GetTotalResults(IEnumerable<Player> players,
                IEnumerable<Cashgame> cashgames)
            {
                var list = players.Select(player => new CashgameTotalResult(player, cashgames)).ToList();
                return list.Where(o => o.GameCount > 0).OrderByDescending(o => o.Winnings).ToList();
            }

            private CashgameTotalResult GetMostTimeResult(IEnumerable<CashgameTotalResult> results)
            {
                CashgameTotalResult mostTimeResult = null;
                foreach (var result in results)
                {
                    if (mostTimeResult == null || result.TimePlayed > mostTimeResult.TimePlayed)
                    {
                        mostTimeResult = result;
                    }
                }
                return mostTimeResult;
            }

            private CashgameTotalResult GetBiggestTotalBuyinResult(IEnumerable<CashgameTotalResult> results)
            {
                CashgameTotalResult biggestTotalBuyinResult = null;
                foreach (var result in results)
                {
                    if (biggestTotalBuyinResult == null || result.Buyin > biggestTotalBuyinResult.Buyin)
                    {
                        biggestTotalBuyinResult = result;
                    }
                }
                return biggestTotalBuyinResult;
            }

            private CashgameTotalResult GetBiggestTotalCashoutResult(IEnumerable<CashgameTotalResult> results)
            {
                CashgameTotalResult biggestTotalCashoutResult = null;
                foreach (var result in results)
                {
                    if (biggestTotalCashoutResult == null || result.Cashout > biggestTotalCashoutResult.Cashout)
                    {
                        biggestTotalCashoutResult = result;
                    }
                }
                return biggestTotalCashoutResult;
            }
        }
    }
}