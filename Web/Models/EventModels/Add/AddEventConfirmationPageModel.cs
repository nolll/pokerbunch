using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Add
{
    public class AddEventConfirmationPageModel : BunchPageModel
    {
        public string BunchName { get; private set; }

        public AddEventConfirmationPageModel(BunchContext.Result contextResult)
            : base("Event Created", contextResult)
        {
            BunchName = contextResult.BunchName;
        }
    }
}