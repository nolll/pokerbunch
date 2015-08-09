using Core.UseCases;
using Core.UseCases.JoinBunchForm;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchPageModel : AppPageModel
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public JoinBunchPageModel(AppContext.Result contextResult, JoinBunchFormResult joinBunchFormResult, JoinBunchPostModel postModel)
            : base("Join Bunch", contextResult)
        {
            Name = joinBunchFormResult.BunchName;
            if (postModel == null) return;
            Code = postModel.Code;
        }
    }
}