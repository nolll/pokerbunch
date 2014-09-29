using Application.Urls;

namespace Application.UseCases.AddUser
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