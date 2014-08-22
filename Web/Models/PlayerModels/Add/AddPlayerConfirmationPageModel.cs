using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerConfirmationPageModel : BunchPageModel
    {
        public string HomegameName { get; private set; }

        public AddPlayerConfirmationPageModel(BunchContextResult contextResult)
            : base("Player Added", contextResult)
        {
            HomegameName = contextResult.BunchName;
        }
    }
}