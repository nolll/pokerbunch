using Core.UseCases.BaseContext;

namespace Web.Models.ErrorModels
{
    public class Error401PageModel : ErrorPageModel
    {
        public Error401PageModel(BaseContextResult contextResult)
            : base(contextResult, "Unauthorized", "You don't have permission to see this page")
        {
        }
    }
}