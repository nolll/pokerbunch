using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

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