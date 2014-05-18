using System;
using Core.Entities.Checkpoints;

namespace Tests.Common.FakeClasses
{
    public class FakeCheckpoint : Checkpoint
    {
        private string _description;

        public FakeCheckpoint(
            DateTime timestamp = default(DateTime),
            CheckpointType type = default(CheckpointType),
            int stack = default(int),
            int amount = default(int),
            int id = default(int),
            string description = default(string))
            : base(
                timestamp,
                type,
                stack,
                amount,
                id)
        {
            _description = description;
        }

        public override string Description
        {
            get { return _description; }
        }
    }
}