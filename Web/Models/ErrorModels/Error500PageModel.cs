using Core.UseCases.BaseContext;

namespace Web.Models.ErrorModels
{
    public class Error500PageModel : ErrorPageModel
    {
        public Error500PageModel(BaseContextResult contextResult)
            : base(contextResult, "Server Error", "An unexpected error occurred")
        {
        }
    }
}