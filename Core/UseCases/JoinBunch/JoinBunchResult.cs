using Core.Urls;

namespace Core.UseCases.JoinBunch
{
    public class JoinBunchResult
    {
        public Url ReturnUrl { get; private set; }

        public JoinBunchResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}