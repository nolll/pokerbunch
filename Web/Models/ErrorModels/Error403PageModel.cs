using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class Error403PageModel : ErrorPageModel
    {
        public Error403PageModel(BaseContext.Result contextResult)
            : base(contextResult, "Access denied", "You don't have permission to see this page")
        {
        }
    }
}