using Core.UseCases;

namespace Web.Models.AdminModels
{
    public class EmailModel
    {
        public string Message { get; }

        public EmailModel(TestEmail.Result result)
        {
            Message = result.Message;
        }
    }
}