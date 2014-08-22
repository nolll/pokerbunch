using Application.UseCases.BaseContext;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel
    {
        public bool EnableAnalytics { get; private set; }

        public GoogleAnalyticsModel(BaseContextResult baseContextResult)
        {
            EnableAnalytics = baseContextResult.IsInProduction;
        }
    }
}