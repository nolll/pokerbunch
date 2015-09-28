using System.Collections.Generic;
using System.Linq;
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
            var ids = _cashgameRepository.FindFinished(bunchId, year);
            return _cashgameRepository.Get(ids);
        }

        public IList<Cashgame> GetByEvent(int eventId)
        {
            var ids = _cashgameRepository.FindByEvent(eventId);
            return _cashgameRepository.Get(ids);
        }

        public Cashgame GetRunning(int bunchId)
        {
            var ids = _cashgameRepository.FindRunning(bunchId);
            return _cashgameRepository.Get(ids).FirstOrDefault();
        }

        public Cashgame GetById(int cashgameId)
        {
            return _cashgameRepository.Get(cashgameId);
        }

        public IList<int> GetYears(int bunchId)
        {
            return _cashgameRepository.GetYears(bunchId);
        }

        public IList<string> GetLocations(int bunchId)
        {
            return _cashgameRepository.GetLocations(bunchId);
        }

        public void DeleteGame(int id)
        {
            _cashgameRepository.DeleteGame(id);
        }

        public int AddGame(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameRepository.AddGame(bunch, cashgame);
        }

        public void UpdateGame(Cashgame cashgame)
        {
            _cashgameRepository.UpdateGame(cashgame);
        }

        public void EndGame(Cashgame cashgame)
        {
            cashgame.ChangeStatus(GameStatus.Finished);
            _cashgameRepository.UpdateGame(cashgame);
        }

        public bool HasPlayed(int playerId)
        {
            var ids = _cashgameRepository.FindByPlayerId(playerId);
            return ids.Any();
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