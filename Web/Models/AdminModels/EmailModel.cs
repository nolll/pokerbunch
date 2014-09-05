using Application.UseCases.TestEmail;

namespace Web.Models.AdminModels
{
    public class EmailModel
    {
        public string Email { get; private set; }

        public EmailModel(TestEmailResult result)
        {
            Email = result.Email;
        }
    }
}