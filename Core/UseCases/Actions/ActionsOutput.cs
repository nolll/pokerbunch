using System;
using System.Collections.Generic;
using Core.Urls;

namespace Core.UseCases.Actions
{
    public class ActionsOutput
    {
        public DateTime Date { get; private set; }
        public string PlayerName { get; private set; }
        public Url ChartDataUrl { get; private set; }
        public IList<CheckpointItem> CheckpointItems { get; private set; }
        
        public ActionsOutput(DateTime date, string playerName, CashgameActionChartJsonUrl chartDataUrl, List<CheckpointItem> checkpointItems)
        {
            Date = date;
            PlayerName = playerName;
            ChartDataUrl = chartDataUrl;
            CheckpointItems = checkpointItems;
        }
    }
}