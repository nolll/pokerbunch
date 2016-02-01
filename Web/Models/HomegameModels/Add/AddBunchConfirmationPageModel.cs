using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddBunchConfirmationPageModel : AppPageModel
    {
        public AddBunchConfirmationPageModel(CoreContext.Result contextResult)
            : base("Bunch Created", contextResult)
        {
        }
    }
}