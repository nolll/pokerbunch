using Core.Settings;

namespace Web.Models.ErrorModels
{
    public class Error401PageModel : ErrorPageModel
    {
        public Error401PageModel(AppSettings appSettings) : base(appSettings)
        {
        }

        public override string Title => "Unauthorized";
        public override string Message => "You don't have permission to see this page";
    }
}