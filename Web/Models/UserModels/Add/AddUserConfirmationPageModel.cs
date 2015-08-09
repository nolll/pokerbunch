using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserConfirmationPageModel : AppPageModel
    {
        public AddUserConfirmationPageModel(AppContext.Result contextResult)
            : base("Homegame Created", contextResult)
        {
        }
    }
}