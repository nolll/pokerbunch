using Core.Urls;

namespace Core.UseCases.DeletePlayer
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