using System;
using Core.Entities.Checkpoints;

namespace Tests.Common.Builders
{
    public class CheckpointBuilder
    {
        private int _cashgameId;
        private int _playerId;
        private DateTime _timestamp;
        private CheckpointType _type;
        private int _stack;
        private int _amount;
        private int _id;

        public CheckpointBuilder()
        {
            _cashgameId = 0;
            _playerId = 0;
            _timestamp = DateTime.MinValue;
            _type = CheckpointType.Report;
            _amount = 0;
            _stack = 0;
            _id = 0;
        }

        public Checkpoint Build()
        {
            return Checkpoint.Create(_cashgameId, _playerId, _timestamp, _type, _stack, _amount, _id);
        }

        public CheckpointBuilder WithCashgameId(int cashgameId)
        {
            _cashgameId = cashgameId;
            return this;
        }

        public CheckpointBuilder WithPlayerId(int playerId)
        {
            _playerId = playerId;
            return this;
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