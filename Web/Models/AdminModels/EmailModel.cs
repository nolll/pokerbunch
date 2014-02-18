namespace Web.Models.AdminModels
{
    public class EmailModel
    {
        public string Email { get; private set; }

        public EmailModel(string email)
        {
            Email = email;
        }
    }
}