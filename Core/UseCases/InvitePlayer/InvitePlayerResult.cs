using Core.Urls;

namespace Core.UseCases.InvitePlayer
{
    public class InvitePlayerResult
    {
        public Url ReturnUrl { get; private set; }

        public InvitePlayerResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}