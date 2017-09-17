using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; }

        public AddPlayerConfirmationPageModel(BunchContext.Result contextResult)
            : base(contextResult)
        {
            BunchName = contextResult.BunchName;
        }

        public override string BrowserTitle => "Player Added";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddPlayer/AddConfirmation.cshtml");
        }
    }
}