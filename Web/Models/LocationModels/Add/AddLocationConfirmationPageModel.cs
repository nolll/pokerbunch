using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Add
{
    public class AddLocationConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; private set; }

        public AddLocationConfirmationPageModel(BunchContext.Result contextResult)
            : base("Location Created", contextResult)
        {
            BunchName = contextResult.BunchName;
        }
    }
}