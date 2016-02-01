using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchPageModel : AppPageModel
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public JoinBunchPageModel(CoreContext.Result contextResult, JoinBunchForm.Result joinBunchFormResult, JoinBunchPostModel postModel)
            : base("Join Bunch", contextResult)
        {
            Name = joinBunchFormResult.BunchName;
            if (postModel == null) return;
            Code = postModel.Code;
        }
    }
}