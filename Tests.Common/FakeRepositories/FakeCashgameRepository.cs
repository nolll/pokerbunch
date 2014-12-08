using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeCashgameRepository : ICashgameRepository
    {
        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            throw new System.NotImplementedException();
        }

        public Cashgame GetRunning(Bunch bunch)
        {
            throw new System.NotImplementedException();
        }

        public Cashgame GetRunning(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public Cashgame GetByDateString(Bunch bunch, string dateString)
        {
            throw new System.NotImplementedException();
        }

        public Cashgame GetById(int cashgameId)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> GetYears(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public IList<string> GetLocations(Bunch bunch)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteGame(Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public int AddGame(Bunch bunch, Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateGame(Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool StartGame(Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool HasPlayed(int playerId)
        {
            throw new System.NotImplementedException();
        }
    }
}