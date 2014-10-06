using Core.UseCases.AppContext;
using Core.UseCases.EditUserForm;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Edit
{
    public class EditUserPageModel : AppPageModel
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }

        public EditUserPageModel(AppContextResult contextResult, EditUserFormResult editUserFormResult, EditUserPostModel postModel)
            : base("Edit Profile", contextResult)
        {
            UserName = editUserFormResult.UserName;
            RealName = editUserFormResult.RealName;
            DisplayName = editUserFormResult.DisplayName;
            Email = editUserFormResult.Email;
            if (postModel == null) return;
            RealName = postModel.RealName;
            DisplayName = postModel.DisplayName;
            Email = postModel.Email;
        }
    }
}