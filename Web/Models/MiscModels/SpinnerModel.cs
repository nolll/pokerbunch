using Web.Extensions;

namespace Web.Models.MiscModels
{
    public class SpinnerModel : IViewModel
    {
        public View GetView()
        {
            return new View("~/Views/Misc/Spinner.cshtml");
        }
    }
}