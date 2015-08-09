using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.ErrorModels
{
    public class ErrorPageModel : WrappedPageModel
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        protected ErrorPageModel(BaseContext.Result contextResult, string title, string message)
            : base("Error", contextResult)
        {
            Title = title;
            Message = message;
        }
    }
}