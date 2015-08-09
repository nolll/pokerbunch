using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddBunchConfirmationPageModel : AppPageModel
    {
        public AddBunchConfirmationPageModel(AppContext.Result contextResult)
            : base("Bunch Created", contextResult)
        {
        }
    }
}