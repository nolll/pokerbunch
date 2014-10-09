using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Tests.Common.Builders
{
    public class CashgameBuilder
    {
        private int _id;
        private int _bunchId;
        private string _location;
        private GameStatus _status;
        private bool _isStarted;
        private DateTime? _startTime;
        private DateTime? _endTime;
        private IList<CashgameResult> _results;
        private int _playerCount;
        private int _diff;
        private int _turnover;
        private bool _hasActivePlayers;
        private int _totalStacks;
        private int _averageBuyin;
        private string _dateString;
        private readonly IList<Checkpoint> _checkpoints;
        private CheckpointBuilder _checkpointBuilder;
        
        public CashgameBuilder()
        {
            _checkpoints = new List<Checkpoint>();
            _checkpointBuilder = new CheckpointBuilder();
        }

        public Cashgame Build()
        {
            return new Cashgame(
                _id, 
                _bunchId,
                _location, 
                _status,
                _isStarted,
                _startTime, 
                _endTime,
                _results,
                _playerCount,
                _diff,
                _turnover,
                _hasActivePlayers,
                _totalStacks,
                _averageBuyin,
                _dateString);
        }

        public CashgameBuilder AddCheckpoint(Checkpoint checkpoint)
        {
            _checkpoints.Add(checkpoint);
            return this;
        }

        public CashgameBuilder WithOnePlayerThatHasBoughtIn()
        {
            var checkpoint = new CheckpointBuilder().OfType(CheckpointType.Buyin).Build();
            _checkpoints.Add(checkpoint);
            return this;
        }

        public CashgameBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CashgameBuilder WithLocation(string location)
        {
            _location = location;
            return this;
        }

        public CashgameBuilder WithDateString(string dateString)
        {
            _dateString = dateString;
            return this;
        }

        public CashgameBuilder WithStartTime(DateTime startTime)
        {
            _startTime = startTime;
            return this;
        }

        public CashgameBuilder WithEndTime(DateTime endTime)
        {
            _endTime = endTime;
            return this;
        }

        public CashgameBuilder WithPlayerCount(int playerCount)
        {
            _playerCount = playerCount;
            return this;
        }

        public CashgameBuilder WithTurnover(int turnover)
        {
            _turnover = turnover;
            return this;
        }

        public CashgameBuilder WithAverageBuyin(int averageBuyin)
        {
            _averageBuyin = averageBuyin;
            return this;
        }

        public CashgameBuilder WithResults(List<CashgameResult> results)
        {
            _results = results;
            return this;
        }

        public CashgameBuilder WithStatus(GameStatus status)
        {
            _status = status;
            return this;
        }

        public CashgameBuilder ThatIsStarted()
        {
            _isStarted = true;
            return this;
        }
    }
}