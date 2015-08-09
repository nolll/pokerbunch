using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class Error500PageModel : ErrorPageModel
    {
        public Error500PageModel(BaseContext.Result contextResult)
            : base(contextResult, "Server Error", "An unexpected error occurred")
        {
        }
    }
}