using Application.Urls;
using Application.UseCaseHelpers;

namespace Application.UseCases.Login
{
    public class LoginResult : UseCaseResultWithValidator
    {
        public LoginResult(Validator validator, Url returnUrl)
            : base(validator)
        {
            ReturnUrl = returnUrl;
        }
    }
}