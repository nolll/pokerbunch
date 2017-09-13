using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchPageModel : AppPageModel
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public JoinBunchPageModel(CoreContext.Result contextResult, JoinBunchForm.Result joinBunchFormResult, string code)
            : base(contextResult)
        {
            Name = joinBunchFormResult.BunchName;
            Code = code;
        }

        public override string BrowserTitle => "Join Bunch";

        public override View GetView()
        {
            return new View("~/Views/Pages/JoinBunch/Join.cshtml");
        }
    }
}