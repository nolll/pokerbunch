using System.Collections.Generic;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Edit
{
    public class EditUserPageModel : AppPageModel
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }
        public ErrorListModel Errors { get; }

        public EditUserPageModel(CoreContext.Result contextResult, EditUserForm.Result editUserFormResult, EditUserPostModel postModel, IEnumerable<string> errors)
            : base(contextResult)
        {
            UserName = editUserFormResult.UserName;
            RealName = editUserFormResult.RealName;
            DisplayName = editUserFormResult.DisplayName;
            Email = editUserFormResult.Email;
            if (postModel == null) return;
            RealName = postModel.RealName;
            DisplayName = postModel.DisplayName;
            Email = postModel.Email;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Edit Profile";

        public override View GetView()
        {
            return new View("~/Views/Pages/EditUser/EditUser.cshtml");
        }
    }
}