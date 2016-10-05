using Core.UseCases;

namespace Web.Models.ErrorModels
{
    public class Error403PageModel : ErrorPageModel
    {
        public override string Title => "Access denied";
        public override string Message => "You don't have permission to see this page";

        public Error403PageModel(BaseContext.Result contextResult)
            : base(contextResult)
        {
        }
    }
}