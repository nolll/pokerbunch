using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.UserModels.Add
{
    public class AddUserConfirmationPageModel : AppPageModel
    {
        public string LoginUrl { get; }

        public AddUserConfirmationPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
            LoginUrl = new LoginUrl().Relative;
        }

        public override string BrowserTitle => "Homegame Created";
    }
}