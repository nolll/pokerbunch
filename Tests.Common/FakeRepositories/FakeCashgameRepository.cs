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
        private readonly IList<Checkpoint> _checkpointList;
        private IList<Cashgame> _list;
        public Cashgame Added { get; private set; }
        public int Deleted { get; private set; }
        public Cashgame Updated { get; private set; }
        public Checkpoint AddedCheckpoint { get; private set; }
        public Checkpoint SavedCheckpoint { get; private set; }
        public Checkpoint DeletedCheckpoint { get; private set; }
        
        public FakeCashgameRepository()
        {
            SetupMultiYear();
            _checkpointList = CreateCheckpointList();
        }

        public Cashgame Get(int cashgameId)
        {
            return _list.FirstOrDefault(o => o.Id == cashgameId);
        }

        public IList<Cashgame> Get(IList<int> ids)
        {
            return _list.Where(o => ids.Contains(o.Id)).ToList();
        }

        public IList<int> FindFinished(int bunchId, int? year = null)
        {
            if (year.HasValue)
                return _list.Where(o => o.StartTime.HasValue && o.StartTime.Value.Year == year && o.Status == GameStatus.Finished).Select(o => o.Id).ToList();
            return _list.Where(o => o.Status == GameStatus.Finished).Select(o => o.Id).ToList();
        }

        public IList<int> FindByEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public IList<int> FindByPlayerId(int playerId)
        {
            var ids = new List<int>();
            foreach (var game in _list)
            {
                if (game.GetResult(playerId) != null)
                {
                    ids.Add(game.Id);
                }
            }
            return ids;
        }

        public IList<int> FindRunning(int bunchId)
        {
            return _list.Where(o => o.Status == GameStatus.Running).Select(o => o.Id).ToList();
        }

        public IList<int> GetYears(int bunchId)
        {
            return _list.Where(o => o.StartTime.HasValue && o.Status == GameStatus.Finished).Select(o => o.StartTime.Value.Year).ToList();
        }

        public IList<string> GetLocations(int bunchId)
        {
            return new[] {TestData.LocationA, TestData.LocationB};
        }

        public bool DeleteGame(int id)
        {
            Deleted = id;
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
            _list.Add(new Cashgame(TestData.BunchA.Id, TestData.LocationC, GameStatus.Running, TestData.CashgameIdC, TestData.RunningGameCheckpoints));
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
        
        public Checkpoint GetCheckpoint(int id)
        {
            return _checkpointList.FirstOrDefault(o => o.Id == id);
        }

        public IList<int> FindCheckpoints(int cashgameId)
        {
            return _checkpointList.Where(o => o.CashgameId == cashgameId).Select(o => o.Id).ToList();
        }

        public IList<int> FindCheckpoints(IList<int> cashgameIds)
        {
            return _checkpointList.Where(o => cashgameIds.Contains(o.Id)).Select(o => o.Id).ToList();
        }

        public int AddCheckpoint(Checkpoint checkpoint)
        {
            AddedCheckpoint = checkpoint;
            return 1;
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            SavedCheckpoint = checkpoint;
            return true;
        }
        
        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            DeletedCheckpoint = checkpoint;
            return true;
        }

        public void SetupRunningGameForCheckpoints()
        {
            ClearCheckpointList();

            foreach (var runningGameCheckpoint in TestData.RunningGameCheckpoints)
            {
                _checkpointList.Add(runningGameCheckpoint);
            }
        }

        private void ClearCheckpointList()
        {
            _checkpointList.Clear();
        }

        private IList<Checkpoint> CreateCheckpointList()
        {
            return new List<Checkpoint>()
            {
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, TestData.BuyinCheckpointTimestamp, CheckpointType.Buyin, TestData.BuyinCheckpointStack, TestData.BuyinCheckpointAmount, TestData.BuyinCheckpointId),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, TestData.ReportCheckpointTimestamp, CheckpointType.Report, TestData.ReportCheckpointStack, TestData.ReportCheckpointAmount, TestData.ReportCheckpointId),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, TestData.CashoutCheckpointTimestamp, CheckpointType.Cashout, TestData.CashoutCheckpointStack, TestData.CashoutCheckpointAmount, TestData.CashoutCheckpointId)
            };
        }
    }
}