using Core.UseCases;
using Web.Common.Urls.SiteUrls;
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

        public UserDetailsPageModel(AppContext.Result contextResult, UserDetails.Result userDetails)
            : base("User Details", contextResult)
        {
            UserName = userDetails.UserName;
            DisplayName = userDetails.DisplayName;
            RealName = userDetails.RealName;
            Email = userDetails.Email;
            AvatarModel = new AvatarModel(userDetails.AvatarUrl);
            ShowEditLink = userDetails.CanEdit;
            ShowPasswordLink = userDetails.CanChangePassword;
            EditUrl = new EditUserUrl(userDetails.UserName).Relative;
            ChangePasswordUrl = new ChangePasswordUrl().Relative;
        }
    }
}