using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserConfirmationPageModel : PageModel
    {
        public AddUserConfirmationPageModel(AppContextResult appContextResult)
            : base("Homegame Created", appContextResult)
        {
        }
    }
}