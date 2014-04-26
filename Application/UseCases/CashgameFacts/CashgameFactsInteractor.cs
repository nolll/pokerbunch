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
            var minutesPlayed = facts.TotalGameTime;
            var turnover = facts.TotalTurnover;
            var bestResult = GetBestResult(facts);
            var worstResult = GetWorstResult(facts);
            var bestTotalResult = GetBestTotalResult(facts);
            var worstTotalResult = GetWorstTotalResult(facts);
            var mostTimeResult = GetMostTimeResult(facts);
            var biggestTotalBuyin = GetBiggestTotalBuyin(facts);
            var biggestTotalCashout = GetBiggestTotalCashout(facts);

            return new CashgameFactsResult
                {
                    GameCount = gameCount,
                    MinutesPlayed = minutesPlayed,
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

        private AmountFact GetBestResult(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.BestResult.PlayerId);
            var amount = facts.BestResult.Winnings;
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetWorstResult(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.WorstResult.PlayerId);
            var amount = facts.WorstResult.Winnings;
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetBestTotalResult(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.BestTotalResult.PlayerId);
            var amount = facts.BestTotalResult.Winnings;
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetWorstTotalResult(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.WorstTotalResult.PlayerId);
            var amount = facts.WorstTotalResult.Winnings;
            return new AmountFact(playerName, amount);
        }

        private DurationFact GetMostTimeResult(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.MostTimeResult.PlayerId);
            var amount = facts.MostTimeResult.TimePlayed;
            return new DurationFact(playerName, amount);
        }

        private AmountFact GetBiggestTotalBuyin(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.BiggestBuyinTotalResult.PlayerId);
            var amount = facts.BiggestBuyinTotalResult.Buyin;
            return new AmountFact(playerName, amount);
        }

        private AmountFact GetBiggestTotalCashout(Core.Classes.CashgameFacts facts)
        {
            var playerName = GetPlayerName(facts.BiggestCashoutTotalResult.PlayerId);
            var amount = facts.BiggestCashoutTotalResult.Cashout;
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
        public int Amount { get; private set; }

        public AmountFact(string playerName, int amount) : base(playerName)
        {
            Amount = amount;
        }
    }
}