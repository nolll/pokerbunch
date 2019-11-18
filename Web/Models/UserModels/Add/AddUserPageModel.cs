using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserPageModel : AppPageModel
    {
        public string UserName { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public ErrorListModel Errors { get; }

        public AddUserPageModel(AppSettings appSettings, CoreContext.Result contextResult, AddUserPostModel postModel, IEnumerable<string> errors)
            : base(appSettings, contextResult)
        {
            if (postModel == null) return;
            UserName = postModel.UserName;
            DisplayName = postModel.DisplayName;
            Email = postModel.Email;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Register";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddUser/AddUser.cshtml");
        }
    }
}