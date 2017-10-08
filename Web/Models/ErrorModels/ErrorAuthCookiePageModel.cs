using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class ErrorAuthCookiePageModel : ErrorPageModel
    {
        public override string Title => "Auth Cookie Error";
        public override string Message => "There was a problem with authentication, please try again";

        public ErrorAuthCookiePageModel(BaseContext.Result contextResult)
            : base(contextResult)
        {
        }
    }
}