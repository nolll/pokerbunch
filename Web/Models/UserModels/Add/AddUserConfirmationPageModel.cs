using Core.Settings;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserConfirmationPageModel : AppPageModel
    {
        public string LoginUrl { get; }

        public AddUserConfirmationPageModel(AppSettings appSettings, CoreContext.Result contextResult)
            : base(appSettings, contextResult)
        {
            LoginUrl = new LoginUrl().Relative;
        }

        public override string BrowserTitle => "Homegame Created";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddUser/AddUserDone.cshtml");
        }
    }
}