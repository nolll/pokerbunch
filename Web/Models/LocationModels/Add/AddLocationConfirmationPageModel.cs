using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Add
{
    public class AddLocationConfirmationPageModel : BunchPageModel
    {
        public AddLocationConfirmationPageModel(AppSettings appSettings, BunchContext.Result contextResult)
            : base(appSettings, contextResult)
        {
        }

        public override string BrowserTitle => "Location Created";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddLocation/AddConfirmation.cshtml");
        }
    }
}