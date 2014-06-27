using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Tests.Common.FakeClasses
{
    public class CashgameResultInTest : CashgameResult
    {
        public CashgameResultInTest(
            int playerId = 0,
            int buyin = 0,
            int winnings = 0,
            IList<Checkpoint> checkpoints = null,
            DateTime? buyinTime = null,
            DateTime? cashoutTime = null,
            int playedTime = 0,
            int stack = 0,
            DateTime? lastReportTime = null, 
            Checkpoint cashoutCheckpoint = null,
            int winRate = 0)
            
            : base(
            playerId,
            buyin, 
            winnings, 
            checkpoints ?? new List<Checkpoint>(), 
            buyinTime,
            cashoutTime, 
            playedTime, 
            stack,
            lastReportTime, 
            cashoutCheckpoint,
            winRate)
        {
        }
    }
}