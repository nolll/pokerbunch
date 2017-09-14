using System.Collections.Generic;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join
{
    public class JoinBunchPageModel : AppPageModel
    {
        public string Name { get; }
        public string Code { get; }
        public ErrorListModel Errors { get; }

        public JoinBunchPageModel(CoreContext.Result contextResult, JoinBunchForm.Result joinBunchFormResult, string code, IEnumerable<string> errors)
            : base(contextResult)
        {
            Name = joinBunchFormResult.BunchName;
            Code = code;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Join Bunch";

        public override View GetView()
        {
            return new View("~/Views/Pages/JoinBunch/Join.cshtml");
        }
    }
}