using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgameResult : CashgameResult
    {
        public FakeCashgameResult(
            Player player = default(Player),
            int buyin = default(int),
            int winnings = default(int),
            IList<Checkpoint> checkpoints = default(IList<Checkpoint>),
            DateTime? buyinTime = default(DateTime?),
            DateTime? cashoutTime = default(DateTime?),
            int playedTime = default(int),
            int stack = default(int),
            DateTime? lastReportTime = default(DateTime?), 
            Checkpoint cashoutCheckpoint = default(Checkpoint), 
            bool hasReported = default(bool))
            : base(
                player, 
                buyin, 
                winnings, 
                checkpoints ?? new List<Checkpoint>(), 
                buyinTime, 
                cashoutTime, 
                playedTime, 
                stack, 
                lastReportTime, 
                cashoutCheckpoint, 
                hasReported)
        {
        }
    }
}