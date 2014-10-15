using Core.Urls;

namespace Core.UseCases.EditUser
{
    public class EditUserResult
    {
        public Url ReturnUrl { get; private set; }

        public EditUserResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}