using Core.Urls;

namespace Core.UseCases.AddUser
{
    public class AddUserResult
    {
        public Url ReturnUrl { get; private set; }

        public AddUserResult()
        {
            ReturnUrl = new AddUserConfirmationUrl();
        }
    }
}