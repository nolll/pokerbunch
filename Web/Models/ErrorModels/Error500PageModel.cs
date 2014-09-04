using Application.UseCases.BaseContext;

namespace Web.Models.ErrorModels
{
    public class Error500PageModel : ErrorPageModel
    {
        public Error500PageModel(BaseContextResult contextResult, string message)
            : base(contextResult, "Server Error", message)
        {
        }
    }
}