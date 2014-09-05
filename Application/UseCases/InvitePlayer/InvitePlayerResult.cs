using Application.Urls;

namespace Application.UseCases.InvitePlayer
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