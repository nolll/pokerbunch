using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeCashgameRepository : ICashgameRepository
    {
        private IList<Cashgame> _list;
        private Cashgame _running;

        public FakeCashgameRepository()
        {
            SetupMultiYear();
        }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            if(year.HasValue)
                return _list.Where(o => o.StartTime.HasValue && o.StartTime.Value.Year == year).ToList();
            return _list;
        }

        public Cashgame GetRunning(int bunchId)
        {
            return _running;
        }

        public Cashgame GetByDateString(int bunchId, string dateString)
        {
            return _list.FirstOrDefault(o => o.DateString == dateString);
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
            foreach (var game in _list)
            {
                if (game.GetResult(playerId) != null)
                    return true;
            }
            return false;
        }

        public void SetupMultiYear()
        {
            _list = GetGames();
        }

        public void SetupSingleYear()
        {
            _list = GetGames(Constants.StartTimeA.AddDays(7));
        }

        private IList<Cashgame> GetGames(DateTime? secondGameStartTime = null)
        {
            var checkpoints1 = new List<Checkpoint>
            {
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdA, Constants.StartTimeA, CheckpointType.Buyin, 200, 200, 1),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdB, Constants.StartTimeA.AddMinutes(1), CheckpointType.Buyin, 200, 200, 2),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdB, Constants.StartTimeA.AddMinutes(30), CheckpointType.Report, 250, 0, 3),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdA, Constants.StartTimeA.AddMinutes(61), CheckpointType.Cashout, 50, 0, 4),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdB, Constants.StartTimeA.AddMinutes(62), CheckpointType.Cashout, 350, 0, 5)
            };

            var startTime2 = secondGameStartTime ?? Constants.StartTimeB;

            var checkpoints2 = new List<Checkpoint>
            {
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdA, startTime2, CheckpointType.Buyin, 200, 200, 1),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdB, startTime2.AddMinutes(1), CheckpointType.Buyin, 200, 200, 2),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdB, startTime2.AddMinutes(2), CheckpointType.Buyin, 200, 200, 3),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdA, startTime2.AddMinutes(30), CheckpointType.Report, 450, 45, 4),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdA, startTime2.AddMinutes(91), CheckpointType.Cashout, 550, 0, 5),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdB, startTime2.AddMinutes(92), CheckpointType.Cashout, 50, 0, 6)
            };

            return new List<Cashgame>
            {
                new Cashgame(Constants.BunchIdA, Constants.LocationA, GameStatus.Finished, Constants.CashgameIdA, checkpoints1),
                new Cashgame(Constants.BunchIdA, Constants.LocationB, GameStatus.Finished, Constants.CashgameIdB, checkpoints2)
            };
        }

        public void SetupRunningGame()
        {
            var checkpoints1 = new List<Checkpoint>
            {
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdA, Constants.StartTimeB.AddDays(7), CheckpointType.Buyin, 200, 200, 1),
                Checkpoint.Create(Constants.CashgameIdA, Constants.PlayerIdB, Constants.StartTimeB.AddDays(7), CheckpointType.Buyin, 200, 200, 2)
            };

            _running = new Cashgame(Constants.BunchIdA, Constants.LocationA, GameStatus.Finished, Constants.CashgameIdA, checkpoints1);
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}