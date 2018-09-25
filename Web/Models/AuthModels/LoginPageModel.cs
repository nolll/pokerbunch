using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.AuthModels
{
    public class LoginPageModel : AppPageModel
    {
        public string AddUserUrl { get; }
        public string ForgotPasswordUrl { get; }

        public LoginPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
            AddUserUrl = new AddUserUrl().Relative;
            ForgotPasswordUrl = new ForgotPasswordUrl().Relative;
        }

        public override string BrowserTitle => "Login";

        public override View GetView()
        {
            return new View("~/Views/Login/Login.cshtml");
        }
    }
}