﻿using System;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.CashgameFacts
{
    public class CashgameFactsInteractor : ICashgameFactsInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameFactsInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public CashgameFactsResult Execute(CashgameFactsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(homegame).OrderBy(o => o.DisplayName).ToList();
            var cashgames = _cashgameRepository.GetPublished(homegame, request.Year);
            var factBuilder = new FactBuilder(cashgames, players);

            var gameCount = factBuilder.GameCount;
            var timePlayed = TimeSpan.FromMinutes(factBuilder.TotalGameTime);
            var turnover = new Money(factBuilder.TotalTurnover, homegame.Currency);
            var bestResult = GetBestResult(factBuilder, homegame.Currency);
            var worstResult = GetWorstResult(factBuilder, homegame.Currency);
            var bestTotalResult = GetBestTotalResult(factBuilder, homegame.Currency);
            var worstTotalResult = GetWorstTotalResult(factBuilder, homegame.Currency);
            var mostTimeResult = GetMostTimeResult(factBuilder);
            var biggestTotalBuyin = GetBiggestTotalBuyin(factBuilder, homegame.Currency);
            var biggestTotalCashout = GetBiggestTotalCashout(factBuilder, homegame.Currency);

            return new CashgameFactsResult
                {
                    GameCount = gameCount,
                    TimePlayed = timePlayed,
                    Turnover = turnover,
                    BestResult = bestResult,
                    WorstResult = worstResult,
                    BestTotalResult = bestTotalResult,
                    WorstTotalResult = worstTotalResult,
                    MostTimeResult = mostTimeResult,
                    BiggestBuyinTotalResult = biggestTotalBuyin,
                    BiggestCashoutTotalResult = biggestTotalCashout
                };
        }

        private AmountFact GetBestResult(FactBuilder facts, Currency currency)
        {
            var playerName = GetPlayerName(facts.BestResult.PlayerId);
            var amount = new Money(facts.BestResult.Winnings, currency);
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetWorstResult(FactBuilder facts, Currency currency)
        {
            var playerName = GetPlayerName(facts.WorstResult.PlayerId);
            var amount = new Money(facts.WorstResult.Winnings, currency);
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetBestTotalResult(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.BestTotalResult.Winnings, currency);
            return new AmountFact(facts.BestTotalResult.Player.DisplayName, amount);
        }

        private AmountFact GetWorstTotalResult(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.WorstTotalResult.Winnings, currency);
            return new AmountFact(facts.WorstTotalResult.Player.DisplayName, amount);
        }

        private DurationFact GetMostTimeResult(FactBuilder facts)
        {
            var amount = facts.MostTimeResult.TimePlayed;
            return new DurationFact(facts.MostTimeResult.Player.DisplayName, amount);
        }

        private AmountFact GetBiggestTotalBuyin(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.BiggestBuyinTotalResult.Buyin, currency);
            return new AmountFact(facts.BiggestBuyinTotalResult.Player.DisplayName, amount);
        }

        private AmountFact GetBiggestTotalCashout(FactBuilder facts, Currency currency)
        {
            var amount = new Money(facts.BiggestCashoutTotalResult.Cashout, currency);
            return new AmountFact(facts.BiggestCashoutTotalResult.Player.DisplayName, amount);
        }

        private string GetPlayerName(int playerId)
        {
            var player = _playerRepository.GetById(playerId);
            return player == null ? string.Empty : player.DisplayName;
        }
    }
}