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

        public IList<Cashgame> ListFinished(string bunchId, int? year = null)
        {
            return _cashgameRepository.ListFinished(bunchId, year);
        }

        public IList<Cashgame> ListByEvent(string eventId)
        {
            return _cashgameRepository.ListByEvent(eventId);
        }

        public Cashgame GetRunning(string bunchId)
        {
            return _cashgameRepository.GetRunning(bunchId);
        }

        public Cashgame GetByCheckpoint(string checkpointId)
        {
            return _cashgameRepository.FindByCheckpoint(checkpointId);
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

        public string Add(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameRepository.Add(bunch, cashgame);
        }

        public void Update(Cashgame cashgame)
        {
            _cashgameRepository.Update(cashgame);
        }

        public void End(Cashgame cashgame)
        {
            cashgame.ChangeStatus(GameStatus.Finished);
            _cashgameRepository.Update(cashgame);
        }

        public bool HasPlayed(string playerId)
        {
            var ids = _cashgameRepository.FindByPlayerId(playerId);
            return ids.Any();
        }
    }

    public static class CashgameService1
    {
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