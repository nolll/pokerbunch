using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.SharingModels
{
    public class SharingIndexPageModel : AppPageModel
    {
        public bool IsSharingToTwitter { get; set; }
        public Url ShareToTwitterSettingsUrl { get; set; }

        public SharingIndexPageModel(AppContextResult contextResult)
            : base("Sharing", contextResult)
        {
        }
    }
}