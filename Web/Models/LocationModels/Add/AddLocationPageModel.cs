using System.Collections.Generic;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Add
{
    public class AddLocationPageModel : BunchPageModel
    {
        public string Name { get; }
        public ErrorListModel Errors { get; }

        public AddLocationPageModel(BunchContext.Result contextResult, AddLocationPostModel postModel = null, IEnumerable<string> errors = null)
            : base(contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Add Location";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddLocation/Add.cshtml");
        }
    }
}