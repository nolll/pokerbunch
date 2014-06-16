using Application.Urls;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.AuthModels
{
    public class LoginPageModel : LoginPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public Url AddUserUrl { get; set; }
        public Url ForgotPasswordUrl { get; set; }
    }
}