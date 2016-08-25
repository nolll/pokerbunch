using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Add
{
    public class AddEventConfirmationPageModel : BunchPageModel
    {
        public AddEventConfirmationPageModel(BunchContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override string BrowserTitle => "Event Created";
    }
}