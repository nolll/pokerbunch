using Core.Classes;
using Core.Factories;
using Core.Repositories;

namespace Core.Services
{
    public class CashgameService : ICashgameService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameSuiteFactory _cashgameSuiteFactory;
        private readonly ICashgameFactsFactory _cashgameFactsFactory;

        public CashgameService(
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            ICashgameSuiteFactory cashgameSuiteFactory,
            ICashgameFactsFactory cashgameFactsFactory)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameSuiteFactory = cashgameSuiteFactory;
            _cashgameFactsFactory = cashgameFactsFactory;
        }

        public CashgameSuite GetSuite(Homegame homegame, int? year = null)
        {
            var players = _playerRepository.GetList(homegame);
            var cashgames = _cashgameRepository.GetPublished(homegame, year);
            return _cashgameSuiteFactory.Create(cashgames, players);
        }

        public CashgameFacts GetFacts(Homegame homegame, int? year = null)
        {
            var players = _playerRepository.GetList(homegame);
            var cashgames = _cashgameRepository.GetPublished(homegame, year);
            return _cashgameFactsFactory.Create(cashgames, players);
        }
    }
}