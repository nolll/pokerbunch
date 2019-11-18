using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddBunchConfirmationPageModel : AppPageModel
    {
        public AddBunchConfirmationPageModel(AppSettings appSettings, CoreContext.Result contextResult)
            : base(appSettings, contextResult)
        {
        }

        public override string BrowserTitle => "Bunch Created";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddBunch/AddBunchConfirmation.cshtml");
        }
    }
}