using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Classes.Checkpoints;

namespace Infrastructure.Factories
{
    public class CashgameResultFactory : ICashgameResultFactory
    {
        public CashgameResult Create(Player player, List<Checkpoint> checkpoints)
        {
            var result = new CashgameResult();

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

            result.Player = player;
            result.Checkpoints = checkpoints;
            result.Buyin = buyin;
            result.Stack = stack;
            result.Winnings = winnings;
            result.BuyinTime = buyinTime;
            result.CashoutTime = cashoutTime;
            result.LastReportTime = lastReportTime;
            result.PlayedTime = playedTime;
            result.CashoutCheckpoint = cashoutCheckpoint;
            result.HasReported = hasReported;

            return result;
        }

        private int GetBuyinSum(List<Checkpoint> checkpoints)
        {
            var buyinCheckpoints = GetCheckpointsOfType(checkpoints, CheckpointType.Buyin);
            var buyin = 0;
            foreach (var checkpoint in buyinCheckpoints)
            {
                buyin += checkpoint.Amount;
            }
            return buyin;
        }

        private List<Checkpoint> GetCheckpointsOfType(IEnumerable<Checkpoint> checkpoints, CheckpointType type)
        {
            var typedCheckpoints = new List<Checkpoint>();
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.Type == type)
                {
                    typedCheckpoints.Add(checkpoint);
                }
            }
            return typedCheckpoints;
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
                return new DateTime();
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