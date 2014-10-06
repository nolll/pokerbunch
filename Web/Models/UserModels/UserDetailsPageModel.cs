using Core.UseCases.AppContext;
using Core.UseCases.UserDetails;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels
{
    public class UserDetailsPageModel : AppPageModel
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }
        public string EditUrl { get; private set; }
        public string ChangePasswordUrl { get; private set; }
        public bool ShowEditLink { get; private set; }
        public bool ShowPasswordLink { get; private set; }
        public AvatarModel AvatarModel { get; private set; }

        public UserDetailsPageModel(AppContextResult contextResult, UserDetailsResult userDetailsResult)
            : base("User Details", contextResult)
        {
            UserName = userDetailsResult.UserName;
            DisplayName = userDetailsResult.DisplayName;
            RealName = userDetailsResult.RealName;
            Email = userDetailsResult.Email;
            AvatarModel = new AvatarModel(userDetailsResult.AvatarUrl);
            ShowEditLink = userDetailsResult.CanEdit;
            ShowPasswordLink = userDetailsResult.CanChangePassword;
            EditUrl = userDetailsResult.EditUrl.Relative;
            ChangePasswordUrl = userDetailsResult.ChangePasswordUrl.Relative;
        }
    }
}