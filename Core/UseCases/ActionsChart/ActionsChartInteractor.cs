using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.ActionsChart
{
    public static class ActionsChartInteractor
    {
        public static ActionsChartResult Execute(
            ITimeProvider timeProvider,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            ActionsChartRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch, request.DateStr);
            var result = cashgame.GetResult(request.PlayerId);
            var now = timeProvider.UtcNow;

            var checkpointItems = GetCheckpointItems(bunch, cashgame, result, now);

            return new ActionsChartResult(checkpointItems);
        }

        private static IList<ActionsChartCheckpointItem> GetCheckpointItems(Bunch bunch, Cashgame cashgame, CashgameResult result, DateTime now)
        {
            var checkpointItems = new List<ActionsChartCheckpointItem>();
            var checkpoints = GetCheckpoints(result);
            var totalBuyin = 0;
            foreach (var checkpoint in checkpoints)
            {
                var stack = checkpoint.Stack;
                var addedMoney = 0;
                if (checkpoint.Type == CheckpointType.Buyin)
                {
                    if (totalBuyin > 0)
                    {
                        addedMoney = checkpoint.Amount;
                    }
                    totalBuyin += checkpoint.Amount;
                }
                var localTime = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
                checkpointItems.Add(new ActionsChartCheckpointItem(localTime, stack, totalBuyin, addedMoney));
            }
            if (cashgame.Status == GameStatus.Running)
            {
                var localTime = TimeZoneInfo.ConvertTime(now, bunch.Timezone);
                checkpointItems.Add(new ActionsChartCheckpointItem(localTime, result.Stack, result.Buyin));
            }
            return checkpointItems;
        }

        private static IEnumerable<Checkpoint> GetCheckpoints(CashgameResult result)
        {
            return PlayerIsInGame(result) ? result.Checkpoints : new List<Checkpoint>();
        }

        private static bool PlayerIsInGame(CashgameResult result)
        {
            return result != null;
        }
    }
}