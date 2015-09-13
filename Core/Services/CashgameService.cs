using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class CashgameService
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public CashgameService(ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository)
        {
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
        }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            return _cashgameRepository.GetFinished(bunchId, year);
        }

        public IList<Cashgame> GetByEvent(int eventId)
        {
            return _cashgameRepository.GetByEvent(eventId);
        }

        public Cashgame GetRunning(int bunchId)
        {
            return _cashgameRepository.GetRunning(bunchId);
        }

        public Cashgame GetById(int cashgameId)
        {
            return _cashgameRepository.GetById(cashgameId);
        }

        public IList<int> GetYears(int bunchId)
        {
            return _cashgameRepository.GetYears(bunchId);
        }

        public IList<string> GetLocations(int bunchId)
        {
            return _cashgameRepository.GetLocations(bunchId);
        }

        public bool DeleteGame(Cashgame cashgame)
        {
            return _cashgameRepository.DeleteGame(cashgame);
        }

        public int AddGame(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameRepository.AddGame(bunch, cashgame);
        }

        public bool UpdateGame(Cashgame cashgame)
        {
            return _cashgameRepository.UpdateGame(cashgame);
        }

        public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameRepository.EndGame(bunch, cashgame);
        }

        public bool HasPlayed(int playerId)
        {
            return _cashgameRepository.HasPlayed(playerId);
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
    }
}