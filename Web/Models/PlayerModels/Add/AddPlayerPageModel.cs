using System.Collections.Generic;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPageModel : BunchPageModel
    {
        public string Name { get; }
        public ErrorListModel Errors { get; }

        public AddPlayerPageModel(BunchContext.Result contextResult, AddPlayerPostModel postModel = null, IEnumerable<string> errors = null)
            : base(contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Add Player";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddPlayer/Add.cshtml");
        }
    }
}