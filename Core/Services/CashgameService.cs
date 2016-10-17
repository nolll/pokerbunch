using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class CashgameService : ICashgameService
    {
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameService(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public IList<Cashgame> GetFinished(string bunchId, int? year = null)
        {
            var ids = _cashgameRepository.FindFinished(bunchId, year);
            return _cashgameRepository.Get(ids);
        }

        public IList<Cashgame> GetByEvent(string eventId)
        {
            var ids = _cashgameRepository.FindByEvent(eventId);
            return _cashgameRepository.Get(ids);
        }

        public Cashgame GetRunning(string bunchId)
        {
            var ids = _cashgameRepository.FindRunning(bunchId);
            return _cashgameRepository.Get(ids).FirstOrDefault();
        }

        public Cashgame GetByCheckpoint(string checkpointId)
        {
            var ids = _cashgameRepository.FindByCheckpoint(checkpointId);
            return _cashgameRepository.Get(ids).FirstOrDefault();
        }

        public Cashgame GetById(string cashgameId)
        {
            return _cashgameRepository.Get(cashgameId);
        }

        public IList<int> GetYears(string bunchId)
        {
            return _cashgameRepository.GetYears(bunchId);
        }

        public void DeleteGame(string id)
        {
            _cashgameRepository.DeleteGame(id);
        }

        public string AddGame(Bunch bunch, Cashgame cashgame)
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

        public bool HasPlayed(string playerId)
        {
            var ids = _cashgameRepository.FindByPlayerId(playerId);
            return ids.Any();
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