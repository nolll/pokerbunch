using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPageModel : BunchPageModel
    {
        public string Name { get; private set; }

        public AddPlayerPageModel(BunchContextResult contextResult, AddPlayerPostModel postModel = null)
            : base("Add Player", contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
        }
    }
}