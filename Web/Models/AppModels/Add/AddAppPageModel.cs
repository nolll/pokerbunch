using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Models.UserModels.Add;

namespace Web.Models.AppModels.Add
{
    public class AddAppPageModel : AppPageModel
    {
        public string AppName { get; private set; }
        
        public AddAppPageModel(CoreContext.Result contextResult, AddAppPostModel postModel)
            : base("Register", contextResult)
        {
            if (postModel == null) return;
            AppName = postModel.AppName;
        }
    }
}