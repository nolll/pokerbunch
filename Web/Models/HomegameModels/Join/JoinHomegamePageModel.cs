using Application.UseCases.AppContext;
using Application.UseCases.JoinBunchForm;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinHomegamePageModel : AppPageModel
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public JoinHomegamePageModel(AppContextResult contextResult, JoinBunchFormResult joinBunchFormResult, JoinHomegamePostModel postModel)
            : base("Join Bunch", contextResult)
        {
            Name = joinBunchFormResult.BunchName;
            if (postModel == null) return;
            Code = postModel.Code;
        }
    }
}