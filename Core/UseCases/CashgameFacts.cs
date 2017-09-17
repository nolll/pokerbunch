﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameFacts
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;
        private readonly IPlayerService _playerService;

        public CashgameFacts(IBunchService bunchService, ICashgameService cashgameService, IPlayerService playerService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);
            var players = _playerService.List(request.Slug).OrderBy(o => o.DisplayName).ToList();
            var cashgames = _cashgameService.List(request.Slug, request.Year).Where(o => !o.IsRunning).ToList();
            var factBuilder = new FactBuilder(cashgames, players);

            return GetFactsResult(_playerService, bunch, factBuilder);
        }

        private static Result GetFactsResult(IPlayerService playerService, Bunch bunch, FactBuilder factBuilder)
        {
            var gameCount = factBuilder.GameCount;
            var timePlayed = Time.FromMinutes(factBuilder.TotalGameTime);
            var turnover = new Money(factBuilder.TotalTurnover, bunch.Currency);
            var bestResult = GetBestResult(playerService, factBuilder, bunch.Currency);
            var worstResult = GetWorstResult(playerService, factBuilder, bunch.Currency);
            var bestTotalResult = GetBestTotalResult(factBuilder, bunch.Currency);
            var worstTotalResult = GetWorstTotalResult(factBuilder, bunch.Currency);
            var mostTimeResult = GetMostTimeResult(factBuilder);
            var biggestTotalBuyin = GetBiggestTotalBuyin(factBuilder, bunch.Currency);
            var biggestTotalCashout = GetBiggestTotalCashout(factBuilder, bunch.Currency);

            return new Result(gameCount, timePlayed, turnover, bestResult, worstResult, bestTotalResult, worstTotalResult, mostTimeResult, biggestTotalBuyin, biggestTotalCashout);
        }

        private static MoneyFact GetBestResult(IPlayerService playerService, FactBuilder facts, Currency currency)
        {
            var playerName = GetPlayerName(playerService, facts.BestResult.Id);
            var amount = new Money(facts.BestResult.Winnings, currency);
            return new MoneyFact(playerName, amount);
        }

        private static MoneyFact GetWorstResult(IPlayerService playerService, FactBuilder facts, Currency currency)
        {
            var playerName = GetPlayerName(playerService, facts.WorstResult.Id);
            var amount = new Money(facts.WorstResult.Winnings, currency);
            return new MoneyFact(playerName, amount);
        }

        private static MoneyFact GetBestTotalResult(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.BestTotalResult.Winnings, currency);
            return new MoneyFact(facts.BestTotalResult.Player.DisplayName, amount);
        }

        private static MoneyFact GetWorstTotalResult(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.WorstTotalResult.Winnings, currency);
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

        private static string GetPlayerName(IPlayerService playerService, string playerId)
        {
            var player = playerService.Get(playerId);
            return player == null ? string.Empty : player.DisplayName;
        }

        public class Request
        {
            public string Slug { get; }
            public int? Year { get; }

            public Request(string slug, int? year)
            {
                Slug = slug;
                Year = year;
            }
        }

        public class Result
        {
            public int GameCount { get; }
            public Time TotalTimePlayed { get; }
            public Money Turnover { get; }
            public MoneyFact BestResult { get; }
            public MoneyFact WorstResult { get; }
            public MoneyFact BestTotalResult { get; }
            public MoneyFact WorstTotalResult { get; }
            public DurationFact MostTimePlayed { get; }
            public MoneyFact BiggestBuyin { get; }
            public MoneyFact BiggestCashout { get; }

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
            public Time Time { get; }

            public DurationFact(string playerName, Time time)
                : base(playerName)
            {
                Time = time;
            }
        }

        public class MoneyFact : PlayerFact
        {
            public Money Amount { get; }

            public MoneyFact(string playerName, Money amount)
                : base(playerName)
            {
                Amount = amount;
            }
        }

        public class PlayerFact
        {
            public string PlayerName { get; }

            protected PlayerFact(string playerName)
            {
                PlayerName = playerName;
            }
        }

        private class FactBuilder
        {
            public int GameCount { get; }
            public ListCashgame.CashgamePlayer BestResult { get; }
            public ListCashgame.CashgamePlayer WorstResult { get; }
            public CashgameTotalResult BestTotalResult { get; }
            public CashgameTotalResult WorstTotalResult { get; }
            public CashgameTotalResult MostTimeResult { get; }
            public CashgameTotalResult BiggestBuyinTotalResult { get; }
            public CashgameTotalResult BiggestCashoutTotalResult { get; }
            public int TotalGameTime { get; }
            public int TotalTurnover { get; }

            public FactBuilder(IList<ListCashgame> cashgames, IEnumerable<Player> players)
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

            private GameData GetGameData(IList<ListCashgame> cashgames)
            {
                ListCashgame.CashgamePlayer bestResult = null;
                ListCashgame.CashgamePlayer worstResult = null;
                var totalGameTime = 0;
                var totalTurnover = 0;

                foreach (var cashgame in cashgames)
                {
                    var results = cashgame.Players;
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
                public int GameCount { get; }
                public ListCashgame.CashgamePlayer BestResult { get; }
                public ListCashgame.CashgamePlayer WorstResult { get; }
                public int TotalGameTime { get; }
                public int TotalTurnover { get; }

                public GameData(int gameCount, ListCashgame.CashgamePlayer bestResult, ListCashgame.CashgamePlayer worstResult, int totalGameTime, int totalTurnover)
                {
                    GameCount = gameCount;
                    BestResult = bestResult;
                    WorstResult = worstResult;
                    TotalGameTime = totalGameTime;
                    TotalTurnover = totalTurnover;
                }
            }

            private IList<CashgameTotalResult> GetTotalResults(IEnumerable<Player> players, IEnumerable<ListCashgame> cashgames)
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