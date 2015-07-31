using System.Collections.Generic;
using System.Linq;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Tests.Common.Builders;

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

        public Checkpoint GetCheckpoint(int id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public int AddCheckpoint(Checkpoint checkpoint)
        {
            Added = checkpoint;
            return 1;
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            Saved = checkpoint;
            return true;
        }
        
        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            Deleted = checkpoint;
            return true;
        }
        
        private IList<Checkpoint> CreateList()
        {
            return new List<Checkpoint>()
            {
                Checkpoint.Create(0, 0, TestData.BuyinCheckpointTimestamp, CheckpointType.Buyin, TestData.BuyinCheckpointStack, TestData.BuyinCheckpointAmount, TestData.BuyinCheckpointId),
                Checkpoint.Create(0, 0, TestData.ReportCheckpointTimestamp, CheckpointType.Report, TestData.ReportCheckpointStack, TestData.ReportCheckpointAmount, TestData.ReportCheckpointId),
                Checkpoint.Create(0, 0, TestData.CashoutCheckpointTimestamp, CheckpointType.Cashout, TestData.CashoutCheckpointStack, TestData.CashoutCheckpointAmount, TestData.CashoutCheckpointId)
            };
        }
    }
}