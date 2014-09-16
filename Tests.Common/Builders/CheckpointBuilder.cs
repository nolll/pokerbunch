using System;
using Core.Entities.Checkpoints;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class CheckpointBuilder
    {
        private int _id;
        private int _stack;
        private int _amount;
        private DateTime _timestamp;
        private CheckpointType _type;

        public CheckpointBuilder()
        {
            _id = 1;
            _stack = 2;
            _amount = 3;
            _timestamp = DateTime.MinValue;
            _type = CheckpointType.Report;
        }

        public Checkpoint Build()
        {
            return new CheckpointInTest(id: _id, stack: _stack, amount: _amount, timestamp: _timestamp, type: _type);
        }

        public CheckpointBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CheckpointBuilder WithStack(int stack)
        {
            _stack = stack;
            return this;
        }

        public CheckpointBuilder WithAmount(int amount)
        {
            _amount = amount;
            return this;
        }

        public CheckpointBuilder WithTimestamp(DateTime timestamp)
        {
            _timestamp = timestamp;
            return this;
        }

        public CheckpointBuilder WithType(CheckpointType type)
        {
            _type = type;
            return this;
        }
    }
}