using System.Collections.Generic;
using System.Linq;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeCheckpointRepository : ICheckpointRepository
    {
        private readonly IList<Checkpoint> _list;

        public Checkpoint Added { get; private set; }
        public Checkpoint Saved { get; private set; }
        public Checkpoint Deleted { get; private set; }

        public FakeCheckpointRepository()
        {
            _list = CreateList();
        }

        public Checkpoint Get(int id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public int Add(Checkpoint checkpoint)
        {
            Added = checkpoint;
            return 1;
        }

        public bool Update(Checkpoint checkpoint)
        {
            Saved = checkpoint;
            return true;
        }
        
        public bool Delete(Checkpoint checkpoint)
        {
            Deleted = checkpoint;
            return true;
        }

        public void SetupRunningGame()
        {
            ClearList();

            foreach (var runningGameCheckpoint in TestData.RunningGameCheckpoints)
            {
                _list.Add(runningGameCheckpoint);
            }
        }

        private void ClearList()
        {
            _list.Clear();
        }
        
        private IList<Checkpoint> CreateList()
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