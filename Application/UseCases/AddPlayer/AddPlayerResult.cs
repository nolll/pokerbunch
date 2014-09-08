using Application.Urls;

namespace Application.UseCases.AddPlayer
{
    public class AddPlayerResult
    {
        public AddPlayerConfirmationUrl ReturnUrl { get; private set; }

        public AddPlayerResult(AddPlayerConfirmationUrl returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}