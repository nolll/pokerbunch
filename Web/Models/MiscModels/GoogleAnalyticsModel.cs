using Core.UseCases;

namespace Web.Models.MiscModels
{
    public class GoogleAnalyticsModel
    {
        public bool EnableAnalytics { get; private set; }

        public GoogleAnalyticsModel(BaseContext.Result baseContextResult)
        {
            EnableAnalytics = baseContextResult.IsInProduction;
        }
    }
}