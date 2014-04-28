using System;
using Core.Classes;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Application.UseCases.CashgameFacts
{
    public class CashgameFactsInteractor : ICashgameFactsInteractor
    {
        private readonly ICashgameService _cashgameService;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameFactsInteractor(
            ICashgameService cashgameService,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository)
        {
            _cashgameService = cashgameService;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
        }

        public CashgameFactsResult Execute(CashgameFactsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var facts = _cashgameService.GetFacts(homegame, request.Year);

            var gameCount = facts.GameCount;
            var timePlayed = TimeSpan.FromMinutes(facts.TotalGameTime);
            var turnover = new Money(facts.TotalTurnover, homegame.Currency);
            var bestResult = GetBestResult(facts, homegame.Currency);
            var worstResult = GetWorstResult(facts, homegame.Currency);
            var bestTotalResult = GetBestTotalResult(facts, homegame.Currency);
            var worstTotalResult = GetWorstTotalResult(facts, homegame.Currency);
            var mostTimeResult = GetMostTimeResult(facts);
            var biggestTotalBuyin = GetBiggestTotalBuyin(facts, homegame.Currency);
            var biggestTotalCashout = GetBiggestTotalCashout(facts, homegame.Currency);

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

        private AmountFact GetBestResult(Core.Classes.CashgameFacts facts, CurrencySettings currency)
        {
            var playerName = GetPlayerName(facts.BestResult.PlayerId);
            var amount = new Money(facts.BestResult.Winnings, currency);
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetWorstResult(Core.Classes.CashgameFacts facts, CurrencySettings currency)
        {
            var playerName = GetPlayerName(facts.WorstResult.PlayerId);
            var amount = new Money(facts.WorstResult.Winnings, currency);
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetBestTotalResult(Core.Classes.CashgameFacts facts, CurrencySettings currency)
        {
            var playerName = GetPlayerName(facts.BestTotalResult.PlayerId);
            var amount = new Money(facts.BestTotalResult.Winnings, currency);
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetWorstTotalResult(Core.Classes.CashgameFacts facts, CurrencySettings currency)
        {
            var playerName = GetPlayerName(facts.WorstTotalResult.PlayerId);
            var amount = new Money(facts.WorstTotalResult.Winnings, currency);
            return new AmountFact(playerName, amount);
        }

        private DurationFact GetMostTimeResult(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.MostTimeResult.PlayerId);
            var amount = facts.MostTimeResult.TimePlayed;
            return new DurationFact(playerName, amount);
        }

        private AmountFact GetBiggestTotalBuyin(Core.Classes.CashgameFacts facts, CurrencySettings currency)
        {
            var playerName = GetPlayerName(facts.BiggestBuyinTotalResult.PlayerId);
            var amount = new Money(facts.BiggestBuyinTotalResult.Buyin, currency);
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetBiggestTotalCashout(Core.Classes.CashgameFacts facts, CurrencySettings currency)
        {
            var playerName = GetPlayerName(facts.BiggestCashoutTotalResult.PlayerId);
            var amount = new Money(facts.BiggestCashoutTotalResult.Cashout, currency);
            return new AmountFact(playerName, amount);
        }

        private string GetPlayerName(int playerId)
        {
            var player = _playerRepository.GetById(playerId);
            return player == null ? string.Empty : player.DisplayName;
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

    public class DurationFact : PlayerFact
    {
        public int Minutes { get; private set; }

        public DurationFact(string playerName, int minutes) : base(playerName)
        {
            Minutes = minutes;
        }
    }

    public class AmountFact : PlayerFact
    {
        public Money Amount { get; private set; }

        public AmountFact(string playerName, Money amount) : base(playerName)
        {
            Amount = amount;
        }
    }

    public class Money
    {
        public int Amount { get; private set; }
        public CurrencySettings Currency { get; private set; }

        public Money(int amount) : this(amount, CurrencySettings.Default)
        {
        }

        public Money(int amount, CurrencySettings currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}