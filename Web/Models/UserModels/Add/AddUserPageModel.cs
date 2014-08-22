using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Add
{
    public class AddUserPageModel : AppPageModel
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        public AddUserPageModel(AppContextResult contextResult, AddUserPostModel postModel)
            : base("Register", contextResult)
        {
            if (postModel == null) return;
            UserName = postModel.UserName;
            DisplayName = postModel.DisplayName;
            Email = postModel.Email;
        }
    }
}