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
        public string Deleted { get; private set; }
        public Cashgame Updated { get; private set; }
        
        public FakeCashgameRepository()
        {
            SetupMultiYear();
        }

        public DetailedCashgame GetDetailedById(string id)
        {
            throw new NotImplementedException();
        }

        public IList<ListCashgame> List(string bunchId, int? year = null)
        {
            throw new NotImplementedException();
        }

        public IList<ListCashgame> EventList(string eventId)
        {
            throw new NotImplementedException();
        }

        public IList<ListCashgame> PlayerList(string playerId)
        {
            throw new NotImplementedException();
        }

        public Cashgame GetRunning(string bunchId)
        {
            return _list.FirstOrDefault(o => o.Status == GameStatus.Running);
        }

        public Cashgame GetByCheckpoint(string checkpointId)
        {
            return _list.FirstOrDefault(o => o.Checkpoints.Any(p => p.Id == checkpointId));
        }

        public IList<int> GetYears(string bunchId)
        {
            return _list.Where(o => o.StartTime.HasValue && o.Status == GameStatus.Finished).Select(o => o.StartTime.Value.Year).ToList();
        }

        public void DeleteGame(string id)
        {
            Deleted = id;
        }

        public string Add(Bunch bunch, Cashgame cashgame)
        {
            Added = cashgame;
            return "1";
        }

        public void Update(Cashgame cashgame)
        {
            Updated = cashgame;
        }

        public DetailedCashgame Update(string id, string locationId, string eventId)
        {
            throw new NotImplementedException();
        }

        public void SetupMultiYear()
        {
            _list = GetGames();
        }

        public void SetupGameCount(int gameCount)
        {
            _list = GetGames(gameCount);
        }

        private IList<Cashgame> GetGames(DateTime? secondGameStartTime = null)
        {
            var checkpoints1 = new List<Checkpoint>
            {
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, TestData.StartTimeA, CheckpointType.Buyin, 200, 200, "1"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, TestData.StartTimeA.AddMinutes(1), CheckpointType.Buyin, 200, 200, "2"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, TestData.StartTimeA.AddMinutes(30), CheckpointType.Report, 250, 0, "3"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, TestData.StartTimeA.AddMinutes(61), CheckpointType.Cashout, 50, 0, "4"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, TestData.StartTimeA.AddMinutes(62), CheckpointType.Cashout, 350, 0, "5")
            };

            var startTime2 = secondGameStartTime ?? TestData.StartTimeB;

            var checkpoints2 = new List<Checkpoint>
            {
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime2, CheckpointType.Buyin, 200, 200, "6"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, startTime2.AddMinutes(1), CheckpointType.Buyin, 200, 200, "7"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, startTime2.AddMinutes(2), CheckpointType.Buyin, 200, 200, "8"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime2.AddMinutes(30), CheckpointType.Report, 450, 45, "9"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime2.AddMinutes(91), CheckpointType.Cashout, 550, 0, "10"),
                Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdB, startTime2.AddMinutes(92), CheckpointType.Cashout, 50, 0, "11")
            };

            return new List<Cashgame>
            {
                new Cashgame(TestData.BunchA.Id, TestData.LocationIdA, GameStatus.Finished, TestData.CashgameIdA, checkpoints1),
                new Cashgame(TestData.BunchA.Id, TestData.LocationIdB, GameStatus.Finished, TestData.CashgameIdB, checkpoints2)
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
                    Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime, CheckpointType.Buyin, 200, 200, "1"),
                    Checkpoint.Create(TestData.CashgameIdA, TestData.PlayerIdA, startTime.AddMinutes(61), CheckpointType.Cashout, 200, 0, "2"),
                };
                games.Add(new Cashgame(TestData.BunchA.Id, TestData.LocationIdA, GameStatus.Finished, TestData.CashgameIdA, checkpoints));
                startTime = startTime.AddDays(1);
            }
            return games;
        }

        public void SetupRunningGame()
        {
            _list.Add(new Cashgame(TestData.BunchA.Id, TestData.LocationIdC, GameStatus.Running, TestData.CashgameIdC, TestData.RunningGameCheckpoints));
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}