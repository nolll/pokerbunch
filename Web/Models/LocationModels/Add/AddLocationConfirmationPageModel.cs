using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Add
{
    public class AddLocationConfirmationPageModel : BunchPageModel
    {
        public AddLocationConfirmationPageModel(BunchContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override string BrowserTitle => "Location Created";
    }
}