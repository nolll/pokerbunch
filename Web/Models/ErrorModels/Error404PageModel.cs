using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class Error404PageModel : ErrorPageModel
    {
        public override string Title => "Page not found";
        public override string Message => "Please check the url for errors";

        public Error404PageModel(BaseContext.Result contextResult)
            : base(contextResult)
        {
        }
    }
}