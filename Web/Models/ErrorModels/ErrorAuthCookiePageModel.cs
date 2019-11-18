using Core.Settings;

namespace Web.Models.ErrorModels
{
    public class ErrorAuthCookiePageModel : ErrorPageModel
    {
        public ErrorAuthCookiePageModel(AppSettings appSettings) : base(appSettings)
        {
        }

        public override string Title => "Auth Cookie Error";
        public override string Message => "There was a problem with authentication, please try again";
    }
}