using System;
using Core.Entities.Checkpoints;
using Core.Factories;

namespace Tests.Common.FakeClasses
{
    public class CheckpointBuilder
    {
        private int _amount;
        private int _stack;
        private DateTime _timestamp;
        private CheckpointType _type;
        private int _id;

        public CheckpointBuilder()
        {
            _amount = 0;
            _stack = 0;
            _timestamp = DateTime.MinValue;
            _type = CheckpointType.Report;
            _id = 0;
        }

        public Checkpoint Build()
        {
            return CheckpointFactory.Create(_timestamp, _type, _stack, _amount, _id);
        }

        public CheckpointBuilder WithAmount(int amount)
        {
            _amount = amount;
            return this;
        }

        public CheckpointBuilder WithStack(int stack)
        {
            _stack = stack;
            return this;
        }

        public CheckpointBuilder WithTimestamp(DateTime dateTime)
        {
            _timestamp = dateTime;
            return this;
        }

        public CheckpointBuilder OfType(CheckpointType type)
        {
            _type = type;
            return this;
        }

        public CheckpointBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
    }
}