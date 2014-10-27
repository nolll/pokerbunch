using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Factories;
using Core.Repositories;

namespace Core.Services
{
    public class CashgameService : ICashgameService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameService(
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
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

        public CashgameSuite GetSuite(int bunchId, int? year = null)
        {
            var players = _playerRepository.GetList(bunchId).OrderBy(o => o.DisplayName).ToList();
            var cashgames = _cashgameRepository.GetFinished(bunchId, year);
            return CashgameSuiteFactory.Create(cashgames, players);
        }

        public IList<Player> GetPlayers(Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(result => result.PlayerId).ToList();
            return _playerRepository.GetList(playerIds);
        }
    }
}