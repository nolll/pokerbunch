using Web.Extensions;

namespace Web.Models.AdminModels
{
    public class ClearCacheModel : IViewModel
    {
        public string Message { get; private set; }

        public ClearCacheModel(string message)
        {
            Message = message;
        }

        public View GetView()
        {
            return new View("~/Views/Admin/ClearCache.cshtml");
        }
    }
}