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
        public Cashgame Added { get; private set; }
        public Cashgame Deleted { get; private set; }
        public Cashgame Ended { get; private set; }
        public Cashgame Updated { get; private set; }

        public FakeCashgameRepository()
        {
            SetupMultiYear();
        }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            if(year.HasValue)
                return _list.Where(o => o.StartTime.HasValue && o.StartTime.Value.Year == year && o.Status == GameStatus.Finished).ToList();
            return _list;
        }

        public IList<Cashgame> GetByEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public Cashgame GetRunning(int bunchId)
        {
            return _list.FirstOrDefault(o => o.Status == GameStatus.Running);
        }

        public Cashgame GetByDateString(int bunchId, string dateString)
        {
            return _list.FirstOrDefault(o => o.DateString == dateString);
        }

        public Cashgame GetById(int cashgameId)
        {
            return _list.FirstOrDefault(o => o.Id == cashgameId);
        }

        public IList<int> GetYears(int bunchId)
        {
            return _list.Where(o => o.StartTime.HasValue && o.Status == GameStatus.Finished).Select(o => o.StartTime.Value.Year).ToList();
        }

        public IList<string> GetLocations(int bunchId)
        {
            return new[] {TestData.LocationA, TestData.LocationB};
        }

        public bool DeleteGame(Cashgame cashgame)
        {
            Deleted = cashgame;
            return true;
        }

        public int AddGame(Bunch bunch, Cashgame cashgame)
        {
            Added = cashgame;
            return 1;
        }

        public bool UpdateGame(Cashgame cashgame)
        {
            Updated = cashgame;
            return true;
        }

        public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            Ended = cashgame;
            return true;
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
            _list = GetGames(TestData.StartTimeA.AddDays(7));
        }

        public void SetupGameCount(int gameCount)
        {
            _list = GetGames(gameCount);
        }

        private IList<Cashgame> GetGames(DateTime? secondGameStartTime = null)
        {
            var checkpoints1 = new List<Checkpoint>
            {
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, TestData.StartTimeA, CheckpointType.Buyin, 200, 200, 1),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, TestData.StartTimeA.AddMinutes(1), CheckpointType.Buyin, 200, 200, 2),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, TestData.StartTimeA.AddMinutes(30), CheckpointType.Report, 250, 0, 3),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, TestData.StartTimeA.AddMinutes(61), CheckpointType.Cashout, 50, 0, 4),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, TestData.StartTimeA.AddMinutes(62), CheckpointType.Cashout, 350, 0, 5)
            };

            var startTime2 = secondGameStartTime ?? TestData.StartTimeB;

            var checkpoints2 = new List<Checkpoint>
            {
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime2, CheckpointType.Buyin, 200, 200, 1),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, startTime2.AddMinutes(1), CheckpointType.Buyin, 200, 200, 2),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, startTime2.AddMinutes(2), CheckpointType.Buyin, 200, 200, 3),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime2.AddMinutes(30), CheckpointType.Report, 450, 45, 4),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime2.AddMinutes(91), CheckpointType.Cashout, 550, 0, 5),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, startTime2.AddMinutes(92), CheckpointType.Cashout, 50, 0, 6)
            };

            return new List<Cashgame>
            {
                new Cashgame(TestData.BunchA.Id, TestData.LocationA, GameStatus.Finished, TestData.CashgameIdA, checkpoints1),
                new Cashgame(TestData.BunchA.Id, TestData.LocationB, GameStatus.Finished, TestData.CashgameIdB, checkpoints2)
            };
        }

        private IList<Cashgame> GetGames(int gameCount)
        {
            var games = new List<Cashgame>();
            var startTime = TestData.StartTimeA;
            for (var i = 0; i < gameCount; i++)
            {
                var checkpoints = new List<Checkpoint>
                {
                    Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime, CheckpointType.Buyin, 200, 200, 1),
                    Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime.AddMinutes(61), CheckpointType.Cashout, 200, 0, 2),
                };
                games.Add(new Cashgame(TestData.BunchA.Id, TestData.LocationA, GameStatus.Finished, TestData.CashgameIdA, checkpoints));
                startTime = startTime.AddDays(1);
            }
            return games;
        }

        public void SetupRunningGame()
        {
            var checkpoints1 = new List<Checkpoint>
            {
                Checkpoint.Create(TestData.CashgameIdC, TestData.PlayerIdA, TestData.StartTimeC, CheckpointType.Buyin, 200, 200, 1),
                Checkpoint.Create(TestData.CashgameIdC, TestData.PlayerIdB, TestData.StartTimeC, CheckpointType.Buyin, 200, 200, 2)
            };

            _list.Add(new Cashgame(TestData.BunchA.Id, TestData.LocationC, GameStatus.Running, TestData.CashgameIdC, checkpoints1));
        }

        public void SetupRunningGameWithCashoutCheckpoint()
        {
            var checkpoints1 = new List<Checkpoint>
            {
                Checkpoint.Create(TestData.CashgameIdC, TestData.PlayerIdA, TestData.StartTimeC, CheckpointType.Buyin, 200, 200, 1),
                Checkpoint.Create(TestData.CashgameIdC, TestData.PlayerIdB, TestData.StartTimeC, CheckpointType.Buyin, 200, 200, 2),
                Checkpoint.Create(TestData.CashgameIdC, TestData.PlayerIdA, TestData.StartTimeC.AddMinutes(1), CheckpointType.Cashout, 200, 0, 3),
            };

            _list.Add(new Cashgame(TestData.BunchA.Id, TestData.LocationC, GameStatus.Running, TestData.CashgameIdC, checkpoints1));
        }

        public void SetupEmptyGame()
        {
            var checkpoints1 = new List<Checkpoint>();
            _list.Clear();
            _list.Add(new Cashgame(TestData.BunchA.Id, TestData.LocationA, GameStatus.Running, TestData.CashgameIdA, checkpoints1));
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}