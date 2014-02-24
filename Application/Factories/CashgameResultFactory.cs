using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.Classes;
using Core.Classes.Checkpoints;

namespace Application.Factories
{
    public class CashgameResultFactory : ICashgameResultFactory
    {
        private readonly ITimeProvider _timeProvider;

        public CashgameResultFactory(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public CashgameResult Create(int playerId, List<Checkpoint> checkpoints)
        {
            var buyin = GetBuyinSum(checkpoints);
            var stack = GetStack(checkpoints);
            var winnings = stack - buyin;
            var buyinTime = GetBuyinTime(checkpoints);
            var lastReportTime = GetLastReportTime(checkpoints);
            var cashoutCheckpoint = GetCashoutCheckpoint(checkpoints);
            DateTime? cashoutTime = null;
            if (cashoutCheckpoint != null)
                cashoutTime = cashoutCheckpoint.Timestamp;
            var playedTime = GetPlayedTime(buyinTime, cashoutTime);
            var hasReported = HasReported(checkpoints);

            return new CashgameResult(
                playerId,
                buyin,
                winnings,
                checkpoints,
                buyinTime,
                cashoutTime,
                playedTime,
                stack,
                lastReportTime,
                cashoutCheckpoint,
                hasReported
                );
        }

        private int GetBuyinSum(IEnumerable<Checkpoint> checkpoints)
        {
            var buyinCheckpoints = GetCheckpointsOfType(checkpoints, CheckpointType.Buyin);
            return buyinCheckpoints.Sum(checkpoint => checkpoint.Amount);
        }

        private List<Checkpoint> GetCheckpointsOfType(IEnumerable<Checkpoint> checkpoints, CheckpointType type)
        {
            return checkpoints.Where(checkpoint => checkpoint.Type == type).ToList();
        }

        private int GetStack(IReadOnlyList<Checkpoint> checkpoints)
        {
            var checkpoint = GetLastCheckpoint(checkpoints);
            return checkpoint != null ? checkpoint.Stack : 0;
        }

        private Checkpoint GetLastCheckpoint(IReadOnlyList<Checkpoint> checkpoints)
        {
            return checkpoints.Count > 0 ? checkpoints[checkpoints.Count - 1] : null;
        }

        private DateTime? GetBuyinTime(IEnumerable<Checkpoint> checkpoints)
        {
            var checkpoint = GetFirstBuyinCheckpoint(checkpoints);
            if (checkpoint == null)
            {
                return null;
            }
            return checkpoint.Timestamp;
        }
        
        private Checkpoint GetFirstBuyinCheckpoint(IEnumerable<Checkpoint> checkpoints)
        {
            return GetCheckpointOfType(checkpoints, CheckpointType.Buyin);
        }

        private Checkpoint GetCashoutCheckpoint(IEnumerable<Checkpoint> checkpoints)
        {
            return GetCheckpointOfType(checkpoints, CheckpointType.Cashout);
        }

        private Checkpoint GetCheckpointOfType(IEnumerable<Checkpoint> checkpoints, CheckpointType type)
        {
            return checkpoints.FirstOrDefault(checkpoint => checkpoint.Type == type);
        }

        private int GetPlayedTime(DateTime? startTime = null, DateTime? endTime = null)
        {
            if (!startTime.HasValue || !endTime.HasValue)
            {
                return 0;
            }
            var timespan = endTime - startTime;
            return (int)Math.Round(timespan.Value.TotalMinutes);
        }

        private DateTime GetLastReportTime(List<Checkpoint> checkpoints)
        {
            var checkpoint = GetLastCheckpoint(checkpoints);
            if (checkpoint == null)
            {
                return _timeProvider.GetTime();
            }
            return checkpoint.Timestamp;
        }

        private bool HasReported(IEnumerable<Checkpoint> checkpoints)
        {
            var reportCheckpoints = GetCheckpointsOfType(checkpoints, CheckpointType.Report);
            return reportCheckpoints.Count > 0;
        }

    }

}