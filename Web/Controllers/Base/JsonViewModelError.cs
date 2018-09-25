using JetBrains.Annotations;

namespace Web.Controllers.Base
{
    public class JsonViewModelError : JsonViewModel
    {
        [UsedImplicitly]
        public string Message { get; }

        public JsonViewModelError(string message)
        {
            Message = message;
        }
    }
}