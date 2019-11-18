using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Add
{
    public class AddEventConfirmationPageModel : BunchPageModel
    {
        public AddEventConfirmationPageModel(AppSettings appSettings, BunchContext.Result contextResult)
            : base(appSettings, contextResult)
        {
        }

        public override string BrowserTitle => "Event Created";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddEvent/AddConfirmation.cshtml");
        }
    }
}