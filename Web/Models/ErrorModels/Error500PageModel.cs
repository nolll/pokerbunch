using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class Error500PageModel : ErrorPageModel
    {
        public override string Title => "Server Error";
        public override string Message => "An unexpected error occurred";

        public Error500PageModel(BaseContext.Result contextResult)
            : base(contextResult)
        {
        }
    }
}