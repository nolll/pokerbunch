using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; private set; }

        public AddPlayerConfirmationPageModel(BunchContext.Result contextResult)
            : base("Player Added", contextResult)
        {
            BunchName = contextResult.BunchName;
        }
    }
}