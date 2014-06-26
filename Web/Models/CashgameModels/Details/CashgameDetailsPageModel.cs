using Application.Services;
using Application.UseCases.BunchContext;
using Application.UseCases.CashgameDetails;
using Core.Entities;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsPageModel : PageModel
    {
        public string Heading { get; private set; }
        public string Duration { get; private set; }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }
        public string Location { get; private set; }
        public bool ShowStartTime { get; private set; }
        public bool ShowEndTime { get; private set; }
        public bool EnableEdit { get; private set; }
        public bool EnableCheckpointsButton { get; private set; }
        public bool DurationEnabled { get; private set; }
        public string EditUrl { get; private set; }
        public string CheckpointsUrl { get; private set; }
        public string ChartDataUrl { get; private set; }
        public string Status { get; private set; }
        public CashgameDetailsTableModel CashgameDetailsTableModel { get; set; }

        public CashgameDetailsPageModel(BunchContextResult contextResult, CashgameDetailsResult detailsResult)
            : base("Cashgame", contextResult)
        {
            Heading = string.Format("Cashgame {0}", detailsResult.Date);
            Location = detailsResult.Location;
            Duration = detailsResult.Duration.ToString();
			DurationEnabled = detailsResult.HasDuration;
            ShowStartTime = detailsResult.HasStartTime;
            StartTime = detailsResult.StartTime.HasValue ? Globalization.FormatTimeStatic(detailsResult.StartTime.Value) : "";
			ShowEndTime = detailsResult.HasEndTime;
            EndTime = detailsResult.EndTime.HasValue ? Globalization.FormatTimeStatic(detailsResult.EndTime.Value) : "";
            Status = GameStatusName.GetName(detailsResult.Status);
            EnableEdit = detailsResult.CanEdit;
            EnableCheckpointsButton = detailsResult.HasCheckpoints;
            EditUrl = detailsResult.EditUrl.Relative;
            CheckpointsUrl = detailsResult.CheckpointsUrl.Relative;
            ChartDataUrl = detailsResult.ChartDataUrl.Relative;
            CashgameDetailsTableModel = new CashgameDetailsTableModel(detailsResult.PlayerItems);
        }
    }
}