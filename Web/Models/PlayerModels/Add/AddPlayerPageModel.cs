using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPageModel : BunchPageModel
    {
        public string Name { get; private set; }

        public AddPlayerPageModel(BunchContext.Result contextResult, AddPlayerPostModel postModel = null)
            : base(contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
        }

        public override string BrowserTitle => "Add Player";
    }
}