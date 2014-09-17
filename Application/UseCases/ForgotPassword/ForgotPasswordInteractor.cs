using Application.Exceptions;
using Application.Urls;

namespace Application.UseCases.ForgotPassword
{
    public static class ForgotPasswordInteractor
    {
        public static ForgotPasswordResult Execute(ForgotPasswordRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var returnUrl = new ForgotPasswordConfirmationUrl();

            return new ForgotPasswordResult(returnUrl);
        }
    }
}