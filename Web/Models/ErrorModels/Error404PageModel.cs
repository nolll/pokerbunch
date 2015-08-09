using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class Error404PageModel : ErrorPageModel
    {
        public Error404PageModel(BaseContext.Result contextResult)
            : base(contextResult, "Page not found", "Please check the url for errors")
        {
        }
    }
}