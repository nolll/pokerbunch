using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Factories.Interfaces;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Core.Services
{
    public class CashgameService : ICashgameService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameSuiteFactory _cashgameSuiteFactory;
        private readonly IBunchRepository _bunchRepository;

        public CashgameService(
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            ICashgameSuiteFactory cashgameSuiteFactory,
            IBunchRepository bunchRepository)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameSuiteFactory = cashgameSuiteFactory;
            _bunchRepository = bunchRepository;
        }

        public static bool SpansMultipleYears(IEnumerable<Cashgame> cashgames)
        {
            var years = new List<int>();
            foreach (var cashgame in cashgames)
            {
                if (cashgame.StartTime.HasValue)
                {
                    var year = cashgame.StartTime.Value.Year;
                    if (!years.Contains(year))
                    {
                        years.Add(year);
                    }
                }
            }
            return years.Count > 1;                
        }

        public CashgameSuite GetSuite(Bunch bunch, int? year = null)
        {
            var players = _playerRepository.GetList(bunch).OrderBy(o => o.DisplayName).ToList();
            var cashgames = _cashgameRepository.GetPublished(bunch, year);
            return _cashgameSuiteFactory.Create(cashgames, players);
        }

        public IList<Player> GetPlayers(Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(result => result.PlayerId).ToList();
            return _playerRepository.GetList(playerIds);
        }

        public bool CashgameIsRunning(string bunchName)
        {
            var homegame = _bunchRepository.GetBySlug(bunchName);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            return cashgame != null;
        }

        public int? GetLatestYear(string slug)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var years = _cashgameRepository.GetYears(homegame);
            if (years.Count == 0)
            {
                return null;
            }
            return years[0];
        }
    }
}