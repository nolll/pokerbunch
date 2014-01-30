using Application.Services.Interfaces;
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
            var host = _webContext.GetHost();
            var isEnabled = host == "pokerbunch.com";

            return new GoogleAnalyticsModel
                {
                    EnableAnalytics = isEnabled
                };
        }
    }
}