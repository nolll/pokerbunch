using Web.Extensions;
using Web.Settings;

namespace Web.Models.MiscModels
{
    public class VueConfigModel : IViewModel
    {
        public string ApiUrl => SiteSettings.ApiUrl;
        public View GetView()
        {
            return new View("~/Views/Misc/VueConfig.cshtml");
        }
    }
}