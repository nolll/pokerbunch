using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeCashgameRepository : ICashgameRepository
    {
        public Cashgame Running { get; set; }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            throw new System.NotImplementedException();
        }

        public Cashgame GetRunning(int bunchId)
        {
            return Running;
        }

        public Cashgame GetByDateString(int bunchId, string dateString)
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

        public IList<string> GetLocations(int bunchId)
        {
            return new[] {Constants.LocationA, Constants.LocationB};
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