using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Add
{
    public class AddEventPageModel : BunchPageModel
    {
        public string Name { get; }
        public ErrorListModel Errors { get;  }

        public AddEventPageModel(AppSettings appSettings, BunchContext.Result contextResult, AddEventPostModel postModel = null, IEnumerable<string> errors = null)
            : base(appSettings, contextResult)
        {
            if (postModel == null) return;
            Name = postModel.Name;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Add Event";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddEvent/Add.cshtml");
        }
    }
}