using Web.Extensions;
using Web.Settings;

namespace Web.Models.MiscModels
{
    public class VueConfigModel : IViewModel
    {
        public string ApiUrl { get; }

        public VueConfigModel(AppSettings appSettings)
        {
            ApiUrl = appSettings.Urls.ApiUri.AbsoluteUri.TrimEnd('/');
        }

        public View GetView()
        {
            return new View("~/Views/Misc/VueConfig.cshtml");
        }
    }
}