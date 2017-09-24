using Core.Services;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Services;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsPageModel : BunchPageModel
    {
        public string Heading { get; }
        public string Duration { get; }
        public string StartTime { get; }
        public string EndTime { get; }
        public string Location { get; }
        public string LocationUrl { get; }
        public bool ShowTime { get; }
        public bool EnableEdit { get; }
        public string EditUrl { get; }
        public string ChartJson { get; }
        public CashgameDetailsTableModel CashgameDetailsTableModel { get; }
        public SpinnerModel SpinnerModel => new SpinnerModel();

        public CashgameDetailsPageModel(BunchContext.Result contextResult, CashgameDetails.Result detailsResult, CashgameDetailsChart.Result cashgameDetailsChartResult)
            : base(contextResult)
        {
            var date = Globalization.FormatShortDate(detailsResult.Date, true);
            var showTime = detailsResult.StartTime < detailsResult.EndTime;
            
            Heading = $"Cashgame {date}";
            Location = detailsResult.LocationName;
            LocationUrl = new LocationDetailsUrl(detailsResult.LocationId).Relative;
            Duration = detailsResult.Duration.ToString();
            ShowTime = showTime;
            StartTime = Globalization.FormatTime(detailsResult.StartTime);
            EndTime = Globalization.FormatTime(detailsResult.EndTime);
            EnableEdit = detailsResult.CanEdit;
            EditUrl = new EditCashgameUrl(detailsResult.CashgameId).Relative;
            CashgameDetailsTableModel = new CashgameDetailsTableModel(detailsResult.PlayerItems);
            ChartJson = JsonHelper.Serialize(new DetailsChartModel(cashgameDetailsChartResult));
        }

        public override string BrowserTitle => "Cashgame";

        public override View GetView()
        {
            return new View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml");
        }
    }
}