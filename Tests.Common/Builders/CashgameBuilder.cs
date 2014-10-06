using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Common.Builders
{
    public class CashgameBuilder
    {
        private int _id;
        private string _location;
        private string _dateString;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _turnover;
        private int _averageBuyin;
        private int _playerCount;
        private IList<Checkpoint> _checkpoints; 

        public CashgameBuilder()
        {
            _id = 1;
            _location = "Location";
            _dateString = "2001-01-01";
            _startTime = new DateTime(2001, 1, 1, 1, 1, 1);
            _endTime = new DateTime(2001, 1, 1, 1, 2, 1);
            _turnover = 2;
            _averageBuyin = 3;
            _playerCount = 4;
            _checkpoints = new List<Checkpoint>();
        }

        public Cashgame Build()
        {
            return new CashgameInTest(
                _id, 
                location: _location, 
                dateString: _dateString, 
                startTime: _startTime, 
                endTime: _endTime,
                turnover: _turnover,
                averageBuyin: _averageBuyin,
                playerCount: _playerCount);
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

        public CashgameBuilder WithCheckpoints(IList<Checkpoint> checkpoints)
        {
            _checkpoints = checkpoints;
            return this;
        }

        public CashgameBuilder AddCheckpoint(Checkpoint checkpoint)
        {
            _checkpoints.Add(checkpoint);
            return this;
        }
    }
}