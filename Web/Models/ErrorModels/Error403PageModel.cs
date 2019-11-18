using Core.Settings;

namespace Web.Models.ErrorModels
{
    public class Error403PageModel : ErrorPageModel
    {
        public Error403PageModel(AppSettings appSettings) : base(appSettings)
        {
        }

        public override string Title => "Access denied";
        public override string Message => "You don't have permission to see this page";
    }
}