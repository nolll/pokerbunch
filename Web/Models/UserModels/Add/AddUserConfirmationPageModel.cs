using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserConfirmationPageModel : AppPageModel
    {
        public AddUserConfirmationPageModel(AppContextResult contextResult)
            : base("Homegame Created", contextResult)
        {
        }
    }
}