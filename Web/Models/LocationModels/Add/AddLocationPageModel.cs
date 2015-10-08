using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Add
{
    public class AddLocationPageModel : BunchPageModel
    {
        public string Name { get; private set; }

        public AddLocationPageModel(BunchContext.Result contextResult, AddLocationPostModel postModel = null)
            : base("Add Location", contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
        }
    }
}