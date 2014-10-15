using Core.Urls;

namespace Core.UseCases.ChangePassword
{
    public class ChangePasswordResult
    {
        public Url ReturnUrl { get; private set; }

        public ChangePasswordResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}