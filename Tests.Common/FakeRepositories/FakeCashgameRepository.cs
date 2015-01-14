using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Tests.Common.Builders;

namespace Tests.Common.FakeRepositories
{
    public class FakeCashgameRepository : ICashgameRepository
    {
        private readonly IList<Cashgame> _list;
        private Cashgame _running;

        public FakeCashgameRepository()
        {
            _list = CreateList();
        }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            return _list;
        }

        public Cashgame GetRunning(int bunchId)
        {
            return _running;
        }

        public Cashgame GetByDateString(int bunchId, string dateString)
        {
            throw _list.FirstOrDefault(o => o.DateString == dateString);
        }

        public Cashgame GetById(int cashgameId)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> GetYears(int bunchId)
        {
            return _list.Where(o => o.StartTime.HasValue).Select(o => o.StartTime.Value.Year).ToList();
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

        public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool HasPlayed(int playerId)
        {
            throw new System.NotImplementedException();
        }

        private IList<Cashgame> CreateList()
        {
            return new List<Cashgame>
            {
                new CashgameBuilder()
                    .WithId(Constants.CashgameIdA)
                    .WithLocation(Constants.LocationA)
                    .WithDateString(Constants.DateStringA)
                    .WithStartTime(Constants.StartTimeA)
                    .WithEndTime(Constants.EndTimeA)
                    .WithPlayerCount(Constants.PlayerCountA)
                    .WithTurnover(Constants.TurnoverA)
                    .WithAverageBuyin(Constants.AvarageBuyinA)
                    .Build(),
                new CashgameBuilder()
                    .WithId(Constants.CashgameIdB)    
                    .WithLocation(Constants.LocationB)
                    .WithDateString(Constants.DateStringB)
                    .WithStartTime(Constants.StartTimeB)
                    .WithEndTime(Constants.EndTimeB)
                    .WithPlayerCount(Constants.PlayerCountB)
                    .WithTurnover(Constants.TurnoverB)
                    .WithAverageBuyin(Constants.AvarageBuyinB)
                    .Build()
            };
        }

        public void SetupRunningGame()
        {
            _running = new CashgameBuilder()
                .ThatIsRunning()
                .Build();
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}