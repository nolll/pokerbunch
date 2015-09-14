using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Core.Services
{
    public class CashgameService
    {
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameService(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
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
        
        public int AddCheckpoint(Checkpoint checkpoint)
        {
            return _cashgameRepository.AddCheckpoint(checkpoint);
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            return _cashgameRepository.UpdateCheckpoint(checkpoint);
        }

        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            return _cashgameRepository.DeleteCheckpoint(checkpoint);
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            return _cashgameRepository.GetCheckpoint(checkpointId);
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