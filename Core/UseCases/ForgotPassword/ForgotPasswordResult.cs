using Core.Urls;

namespace Core.UseCases.ForgotPassword
{
    public class ForgotPasswordResult
    {
        public Url ReturnUrl { get; private set; }

        public ForgotPasswordResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}