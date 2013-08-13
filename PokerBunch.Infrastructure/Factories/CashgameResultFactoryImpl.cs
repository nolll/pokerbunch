using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using entities;

namespace Infrastructure.Factories
{
    public class CashgameResultFactoryImpl : CashgameResultFactory
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
            var buyinCheckpoints = GetCheckpointsOfType(checkpoints, typeof (BuyinCheckpoint));
            var buyin = 0;
            foreach (var checkpoint in buyinCheckpoints)
            {
                buyin += checkpoint.Amount;
            }
            return buyin;
        }

        private List<Checkpoint> GetCheckpointsOfType(List<Checkpoint> checkpoints, Type type)
        {
            var typedCheckpoints = new List<Checkpoint>();
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.GetType() == type)
                {
                    typedCheckpoints.Add(checkpoint);
                }
            }
            return typedCheckpoints;
        }

        private int GetStack(List<Checkpoint> checkpoints)
        {
            var checkpoint = GetLastCheckpoint(checkpoints);
            return checkpoint != null ? checkpoint.Stack : 0;
        }

        private Checkpoint GetLastCheckpoint(List<Checkpoint> checkpoints)
        {
            return checkpoints.Count > 0 ? checkpoints[checkpoints.Count - 1] : null;
        }

        private DateTime? GetBuyinTime(List<Checkpoint> checkpoints)
        {
            var checkpoint = GetFirstBuyinCheckpoint(checkpoints);
            if (checkpoint == null)
            {
                return null;
            }
            return checkpoint.Timestamp;
        }
        
        private Checkpoint GetFirstBuyinCheckpoint(List<Checkpoint> checkpoints)
        {
            return GetCheckpointOfType(checkpoints, typeof (BuyinCheckpoint));
        }

        private Checkpoint GetCashoutCheckpoint(List<Checkpoint> checkpoints)
        {
            return GetCheckpointOfType(checkpoints, typeof (CashoutCheckpoint));
        }

        private Checkpoint GetCheckpointOfType(List<Checkpoint> checkpoints, Type type)
        {
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.GetType() == type)
                {
                    return checkpoint;
                }
            }
            return null;
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

        public bool HasReported(List<Checkpoint> checkpoints)
        {
            var reportCheckpoints = GetCheckpointsOfType(checkpoints, typeof (ReportCheckpoint));
            return reportCheckpoints.Count > 0;
        }

    }

}