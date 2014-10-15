using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Core.UseCases.CashgameDetailsChart
{
    public class CashgameDetailsChartInteractor
    {
        public static CashgameDetailsChartResult Execute(
            ITimeProvider timeProvider,
            ICashgameService cashgameService,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            CashgameDetailsChartRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch, request.DateStr);
            var players = cashgameService.GetPlayers(cashgame).OrderBy(o => o.Id).ToList();
            var now = timeProvider.UtcNow;

            var playerItems = GetPlayerItems(bunch, cashgame, players, now);

            return new CashgameDetailsChartResult(playerItems);
        }

        private static IList<DetailsChartPlayerItem> GetPlayerItems(Bunch bunch, Cashgame cashgame, IList<Player> players, DateTime now)
        {
            var playerItems = new List<DetailsChartPlayerItem>();
            foreach (var player in players)
            {
                var result = cashgame.GetResult(player.Id);
                var resultItems = new List<DetailsChartResultItem>();
                var totalBuyin = 0;
                var checkpoints = result.Checkpoints;
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.Type == CheckpointType.Buyin)
                    {
                        totalBuyin += checkpoint.Amount;
                    }
                    var localTime = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
                    var winnings = checkpoint.Stack - totalBuyin;
                    resultItems.Add(new DetailsChartResultItem(localTime, winnings));
                }
                if (cashgame.Status == GameStatus.Running)
                {
                    var timestamp = TimeZoneInfo.ConvertTime(now, bunch.Timezone);
                    resultItems.Add(new DetailsChartResultItem(timestamp, result.Winnings));
                }
                playerItems.Add(new DetailsChartPlayerItem(player.Id, player.DisplayName, resultItems));
            }
            return playerItems;
        }
    }
}