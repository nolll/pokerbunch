using Application.UseCases.BaseContext;
using Web.Models.PageBaseModels;

namespace Web.Models.ErrorModels
{
    public class ErrorPageModel : PageModel
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        protected ErrorPageModel(BaseContextResult contextResult, string title, string message)
            : base("Error", contextResult)
        {
            Title = title;
            Message = message;
        }
    }
}