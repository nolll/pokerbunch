using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.SharingModels
{
    public class SharingIndexPageModel : PageModel
    {
        public bool IsSharingToTwitter { get; set; }
        public Url ShareToTwitterSettingsUrl { get; set; }

        public SharingIndexPageModel(AppContextResult appContextResult)
            : base("Sharing", appContextResult)
        {
        }
    }
}