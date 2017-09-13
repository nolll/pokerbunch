using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Add
{
    public class AddLocationPageModel : BunchPageModel
    {
        public string Name { get; }

        public AddLocationPageModel(BunchContext.Result contextResult, AddLocationPostModel postModel = null)
            : base(contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
        }

        public override string BrowserTitle => "Add Location";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddLocation/Add.cshtml");
        }
    }
}