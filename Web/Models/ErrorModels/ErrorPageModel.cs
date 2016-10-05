using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.ErrorModels
{
    public abstract class ErrorPageModel : WrappedPageModel
    {
        public abstract string Title { get; }
        public abstract string Message { get; }
        public override string BrowserTitle => "Error";

        protected ErrorPageModel(BaseContext.Result contextResult)
            : base(contextResult)
        {
        }
    }
}