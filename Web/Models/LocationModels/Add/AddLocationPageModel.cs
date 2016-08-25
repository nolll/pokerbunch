using Core.UseCases;
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
    }
}