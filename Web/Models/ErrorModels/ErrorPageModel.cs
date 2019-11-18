using Core.Settings;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.ErrorModels
{
    public abstract class ErrorPageModel : WrappedPageModel
    {
        protected ErrorPageModel(AppSettings appSettings) : base(appSettings)
        {
        }

        public abstract string Title { get; }
        public abstract string Message { get; }
        public override string BrowserTitle => "Error";

        public override View GetView()
        {
            return new View("~/Views/Error/Error.cshtml");
        }
    }
}