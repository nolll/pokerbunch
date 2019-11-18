using Core.Settings;

namespace Web.Models.ErrorModels
{
    public class Error500PageModel : ErrorPageModel
    {
        public Error500PageModel(AppSettings appSettings) : base(appSettings)
        {
        }

        public override string Title => "Server Error";
        public override string Message => "An unexpected error occurred";
    }
}