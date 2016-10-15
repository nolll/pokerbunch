using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class Error401PageModel : ErrorPageModel
    {
        public override string Title => "Unauthorized";
        public override string Message => "You don't have permission to see this page";

        public Error401PageModel(BaseContext.Result contextResult)
            : base(contextResult)
        {
        }
    }
}