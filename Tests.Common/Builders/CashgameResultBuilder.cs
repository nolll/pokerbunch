using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Tests.Common.Builders
{
    public class CashgameResultBuilder
    {
        private IList<Checkpoint> _checkpoints;
        private int _playerId;

        public CashgameResultBuilder()
        {
            _checkpoints = new List<Checkpoint>();
        }

        public CashgameResult Build()
        {
            return new CashgameResult(_playerId, _checkpoints);
        }

        public CashgameResultBuilder WithCheckpoints(List<Checkpoint> checkpoints)
        {
            _checkpoints = checkpoints;
            return this;
        }

        public CashgameResultBuilder WithPlayerId(int playerId)
        {
            _playerId = playerId;
            return this;
        }
    }
}