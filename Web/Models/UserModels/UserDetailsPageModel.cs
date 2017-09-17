using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.UserModels
{
    public class UserDetailsPageModel : AppPageModel
    {
        public string UserName { get; }
        public string DisplayName { get; }
        public string RealName { get; }
        public string Email { get; }
        public string EditUrl { get; }
        public string ChangePasswordUrl { get; }
        public bool ShowEditLink { get; }
        public bool ShowPasswordLink { get; }
        public AvatarModel AvatarModel { get; }

        public UserDetailsPageModel(CoreContext.Result contextResult, UserDetails.Result userDetails)
            : base(contextResult)
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

        public override string BrowserTitle => "User Details";

        public override View GetView()
        {
            return new View("~/Views/Pages/UserDetails/UserDetails.cshtml");
        }
    }
}