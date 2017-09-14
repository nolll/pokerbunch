using System.Collections.Generic;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.Add
{
    public class AddAppPageModel : AppPageModel
    {
        public string AppName { get; }
        public ErrorListModel Errors { get; }

        public AddAppPageModel(CoreContext.Result contextResult, AddAppPostModel postModel, IEnumerable<string> errors)
            : base(contextResult)
        {
            if (postModel == null) return;
            AppName = postModel.AppName;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Register";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddApp/AddApp.cshtml");
        }
    }
}