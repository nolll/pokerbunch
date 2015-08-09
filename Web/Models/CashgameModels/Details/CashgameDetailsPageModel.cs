using Core.Services;
using Core.UseCases;
using Core.UseCases.CashgameDetailsChart;
using Newtonsoft.Json;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsPageModel : BunchPageModel
    {
        public string Heading { get; private set; }
        public string Duration { get; private set; }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }
        public string Location { get; private set; }
        public bool ShowStartTime { get; private set; }
        public bool ShowEndTime { get; private set; }
        public bool EnableEdit { get; private set; }
        public bool DurationEnabled { get; private set; }
        public string EditUrl { get; private set; }
        public string ChartJson { get; private set; }
        public CashgameDetailsTableModel CashgameDetailsTableModel { get; private set; }

        public CashgameDetailsPageModel(BunchContext.Result contextResult, CashgameDetails.Result detailsResult, CashgameDetailsChartResult cashgameDetailsChartResult)
            : base("Cashgame", contextResult)
        {
            var date = Globalization.FormatShortDate(detailsResult.Date, true);
            var showStartTime = detailsResult.StartTime.HasValue;
            var showEndTime = detailsResult.EndTime.HasValue;
            var showDuration = showStartTime && showEndTime;
            
            Heading = string.Format("Cashgame {0}", date);
            Location = detailsResult.Location;
            Duration = detailsResult.Duration.String;
			DurationEnabled = showDuration;
            ShowStartTime = showStartTime;
            StartTime = showStartTime ? Globalization.FormatTime(detailsResult.StartTime.Value) : "";
			ShowEndTime = showEndTime;
            EndTime = showEndTime ? Globalization.FormatTime(detailsResult.EndTime.Value) : "";
            EnableEdit = detailsResult.CanEdit;
            EditUrl = detailsResult.EditUrl.Relative;
            CashgameDetailsTableModel = new CashgameDetailsTableModel(detailsResult.PlayerItems);
            ChartJson = JsonConvert.SerializeObject(new DetailsChartModel(cashgameDetailsChartResult));
        }
    }
}