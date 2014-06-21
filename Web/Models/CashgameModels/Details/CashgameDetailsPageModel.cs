using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsPageModel : PageModel
    {
        public string Heading { get; set; }
        public string Duration { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
        public bool ShowStartTime { get; set; }
        public bool ShowEndTime { get; set; }
        public bool EnableEdit { get; set; }
        public bool EnableCheckpointsButton { get; set; }
        public bool DurationEnabled { get; set; }
        public Url EditUrl { get; set; }
        public Url CheckpointsUrl { get; set; }
        public Url ChartDataUrl { get; set; }
        public string Status { get; set; }
        public CashgameDetailsTableModel CashgameDetailsTableModel { get; set; }

        public CashgameDetailsPageModel(BunchContextResult contextResult)
            : base("Cashgame", contextResult)
        {
        }
    }
}