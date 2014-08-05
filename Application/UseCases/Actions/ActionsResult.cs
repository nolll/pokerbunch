using System;
using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Core.Entities;

namespace Application.UseCases.Actions
{
    public class ActionsResult
    {
        public DateTime Date { get; private set; }
        public string PlayerName { get; private set; }
        public Url ChartDataUrl { get; private set; }
        public IList<CheckpointItem> CheckpointItems { get; private set; }

        public ActionsResult(Homegame homegame, Cashgame cashgame, Player player, bool isManager, CashgameResult playerResult)
        {
            var chartDataUrl = new CashgameActionChartJsonUrl(homegame.Slug, cashgame.DateString, player.Id);
            var checkpointItems = playerResult.Checkpoints.Select(o => new CheckpointItem(homegame, cashgame, player, isManager, o)).ToList();

            Date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.MinValue;
            PlayerName = player.DisplayName;
            ChartDataUrl = chartDataUrl;
            CheckpointItems = checkpointItems;
        }
    }
}