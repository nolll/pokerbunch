using Core.UseCases.BaseContext;

namespace Web.Models.ErrorModels
{
    public class Error403PageModel : ErrorPageModel
    {
        public Error403PageModel(BaseContextResult contextResult)
            : base(contextResult, "Forbidden", "You don't have permission to see this page")
        {
        }
    }
}