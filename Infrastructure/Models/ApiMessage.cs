using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiMessage
    {
        [UsedImplicitly]
        public string Message { get; set; }

        public ApiMessage(string message)
        {
            Message = message;
        }

        public ApiMessage()
        {
        }
    }
}