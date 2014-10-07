using Core.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; private set; }

        public AddPlayerConfirmationPageModel(BunchContextResult contextResult)
            : base("Player Added", contextResult)
        {
            BunchName = contextResult.BunchName;
        }
    }
}