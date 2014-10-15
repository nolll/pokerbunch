using Core.Urls;

namespace Core.UseCases.EditBunch
{
    public class EditBunchResult
    {
        public Url ReturnUrl { get; private set; }

        public EditBunchResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}