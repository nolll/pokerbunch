using Core.Urls;

namespace Core.UseCases.AddBunch
{
    public class AddBunchResult
    {
        public Url ReturnUrl { get; private set; }

        public AddBunchResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}