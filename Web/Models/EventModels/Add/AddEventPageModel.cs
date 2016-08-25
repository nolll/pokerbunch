using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Add
{
    public class AddEventPageModel : BunchPageModel
    {
        public string Name { get; private set; }

        public AddEventPageModel(BunchContext.Result contextResult, AddEventPostModel postModel = null)
            : base(contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
        }

        public override string BrowserTitle => "Add Event";
    }
}