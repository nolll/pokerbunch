using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Factories;

namespace Infrastructure.Services
{
    public class CashgameService : ICashgameService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameSuiteFactory _cashgameSuiteFactory;
        private readonly ICashgameTotalResultFactory _cashgameTotalResultFactory;

        public CashgameService(
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            ICashgameSuiteFactory cashgameSuiteFactory,
            ICashgameTotalResultFactory cashgameTotalResultFactory)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameSuiteFactory = cashgameSuiteFactory;
            _cashgameTotalResultFactory = cashgameTotalResultFactory;
        }

        public CashgameSuite GetSuite(Homegame homegame, int? year = null)
        {
            var players = _playerRepository.GetAll(homegame);
            var cashgames = _cashgameRepository.GetPublished(homegame, year);
            return _cashgameSuiteFactory.Create(cashgames, players);
        }

        public IList<CashgameTotalResult> GetTotalResults()
        {
            var players = _playerRepository.GetAll(homegame);
            var cashgames = _cashgameRepository.GetPublished(homegame, year);
            return _cashgameTotalResultFactory.Create()
        }
    }
}