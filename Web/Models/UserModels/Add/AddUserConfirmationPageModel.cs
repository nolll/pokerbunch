using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserConfirmationPageModel : AppPageModel
    {
        public AddUserConfirmationPageModel(CoreContext.Result contextResult)
            : base("Homegame Created", contextResult)
        {
        }
    }
}