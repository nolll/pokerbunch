using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddHomegameConfirmationPageModel : AppPageModel
    {
        public AddHomegameConfirmationPageModel(AppContextResult contextResult)
            : base("Homegame Created", contextResult)
        {
        }
    }
}