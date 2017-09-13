using Core.UseCases;
using Web.Extensions;

namespace Web.Models.AdminModels
{
    public class EmailModel : IViewModel
    {
        public string Message { get; }

        public EmailModel(TestEmail.Result result)
        {
            Message = result.Message;
        }

        public View GetView()
        {
            return new View("~/Views/Admin/Email.cshtml");
        }
    }
}