using Application.UseCases.AppContext;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel
    {
        public bool EnableAnalytics { get; private set; }

        public GoogleAnalyticsModel(AppContextResult appContextResult)
        {
            EnableAnalytics = appContextResult.IsInProduction;
        }
    }
}