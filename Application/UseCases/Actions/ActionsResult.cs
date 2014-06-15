using System.Collections.Generic;
using Application.Urls;

namespace Application.UseCases.Actions
{
    public class ActionsResult
    {
        public string Date { get; set; }
        public string PlayerName { get; set; }
        public Url ChartDataUrl { get; set; }
        public IList<CheckpointItem> CheckpointItems { get; set; }
    }
}