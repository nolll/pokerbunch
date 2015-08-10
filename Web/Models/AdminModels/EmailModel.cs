using Core.UseCases;

namespace Web.Models.AdminModels
{
    public class EmailModel
    {
        public string Email { get; private set; }

        public EmailModel(TestEmail.Result result)
        {
            Email = result.Email;
        }
    }
}