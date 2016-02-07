using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserConfirmationPageModel : AppPageModel
    {
        public string LoginUrl { get; }

        public AddUserConfirmationPageModel(CoreContext.Result contextResult)
            : base("Homegame Created", contextResult)
        {
            LoginUrl = new LoginUrl().Relative;
        }
    }
}