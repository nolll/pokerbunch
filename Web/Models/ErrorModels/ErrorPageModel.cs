using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.ErrorModels
{
    public abstract class ErrorPageModel : WrappedPageModel
    {
        public abstract string Title { get; }
        public abstract string Message { get; }

        protected ErrorPageModel(BaseContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override string BrowserTitle => "Error";

        public override View GetView()
        {
            return new View("~/Views/Error/Error.cshtml");
        }
    }
}