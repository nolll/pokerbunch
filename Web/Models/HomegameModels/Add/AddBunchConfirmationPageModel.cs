using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddBunchConfirmationPageModel : AppPageModel
    {
        public AddBunchConfirmationPageModel(AppContextResult contextResult)
            : base("Bunch Created", contextResult)
        {
        }
    }
}