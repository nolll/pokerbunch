using Application.Urls;
using Application.UseCaseHelpers;

namespace Application.UseCases.InvitePlayer
{
    public class InvitePlayerResult : UseCaseResultWithValidator
    {
        public InvitePlayerResult(Validator validator, Url returnUrl)
            : base(validator)
        {
            ReturnUrl = returnUrl;
        }
    }
}