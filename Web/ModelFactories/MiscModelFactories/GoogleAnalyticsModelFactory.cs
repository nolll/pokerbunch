using Application.Services;
using Web.Models.MiscModels;

namespace Web.ModelFactories.MiscModelFactories
{
    public class GoogleAnalyticsModelFactory : IGoogleAnalyticsModelFactory
    {
        private readonly IWebContext _webContext;

        public GoogleAnalyticsModelFactory(IWebContext webContext)
        {
            _webContext = webContext;
        }

        public GoogleAnalyticsModel Create()
        {
            var host = _webContext.Host;
            var isEnabled = host == "pokerbunch.com";

            return new GoogleAnalyticsModel
                {
                    EnableAnalytics = isEnabled
                };
        }
    }
}