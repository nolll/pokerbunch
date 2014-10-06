using Core.UseCases.BaseContext;

namespace Web.Models.ErrorModels
{
    public class Error404PageModel : ErrorPageModel
    {
        public Error404PageModel(BaseContextResult contextResult)
            : base(contextResult, "Page not found", "Please check the url for errors")
        {
        }
    }
}