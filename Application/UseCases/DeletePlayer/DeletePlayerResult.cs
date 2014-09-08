using Application.Urls;

namespace Application.UseCases.DeletePlayer
{
    public class DeletePlayerResult
    {
        public Url ReturnUrl { get; private set; }

        public DeletePlayerResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}