using Application.UseCases.CashgameContext;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel
    {
        public bool EnableAnalytics { get; set; }

        public GoogleAnalyticsModel()
        {
        }

        public GoogleAnalyticsModel(ApplicationContextResult applicationContextResult)
        {
            EnableAnalytics = applicationContextResult.IsInProduction;
        }
    }
}