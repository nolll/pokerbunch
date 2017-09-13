using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.Add
{
    public class AddAppPageModel : AppPageModel
    {
        public string AppName { get; }
        
        public AddAppPageModel(CoreContext.Result contextResult, AddAppPostModel postModel)
            : base(contextResult)
        {
            if (postModel == null) return;
            AppName = postModel.AppName;
        }

        public override string BrowserTitle => "Register";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddApp/AddApp.cshtml");
        }
    }
}