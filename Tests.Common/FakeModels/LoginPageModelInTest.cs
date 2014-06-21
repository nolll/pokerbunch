using Application.UseCases.AppContext;
using Tests.Common.FakeClasses;
using Web.Models.AuthModels;

namespace Tests.Common.FakeModels
{
    public class LoginPageModelInTest : LoginPageModel
    {
        public LoginPageModelInTest(
            AppContextResult contextResult = null,
            string returnUrl = null,
            LoginPostModel postModel = null)
            
            : base(
                contextResult ?? new AppContextResultInTest(),
                returnUrl,
                postModel)
        {
        }
    }
}