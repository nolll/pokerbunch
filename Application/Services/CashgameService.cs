using System.Collections.Generic;
using System.Linq;
using Application.Factories.Interfaces;
using Core.Classes;
using Core.Repositories;

namespace Application.Services
{
    public class CashgameService : ICashgameService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameSuiteFactory _cashgameSuiteFactory;
        private readonly ICashgameFactsFactory _cashgameFactsFactory;
        private readonly IHomegameRepository _homegameRepository;

        public CashgameService(
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            ICashgameSuiteFactory cashgameSuiteFactory,
            ICashgameFactsFactory cashgameFactsFactory,
            IHomegameRepository homegameRepository)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameSuiteFactory = cashgameSuiteFactory;
            _cashgameFactsFactory = cashgameFactsFactory;
            _homegameRepository = homegameRepository;
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

        public IList<Player> GetPlayers(Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(result => result.PlayerId).ToList();
            return _playerRepository.GetList(playerIds);
        }

        public bool CashgameIsRunning(string bunchName)
        {
            var homegame = _homegameRepository.GetByName(bunchName);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            return cashgame != null;
        }

        public int? GetLatestYear(string slug)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var years = _cashgameRepository.GetYears(homegame);
            if (years.Count == 0)
            {
                return null;
            }
            return years[0];
        }
    }
}