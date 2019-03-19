using Web.Extensions;
using Web.Services;
using Web.Settings;

namespace Web.Models.MiscModels
{
    public class VueConfigModel : IViewModel
    {
        public string ApiHost => SiteSettings.ApiHost;
        public View GetView()
        {
            return new View("~/Views/Misc/VueConfig.cshtml");
        }
    }
}