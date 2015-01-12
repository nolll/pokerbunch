using Core.Urls;

namespace Core.UseCases.UserDetails
{
    public class UserDetailsResult
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }
        public string AvatarUrl { get; private set; }
        public bool CanEdit { get; private set; }
        public bool CanChangePassword { get; private set; }
        public Url EditUrl { get; private set; }
        public Url ChangePasswordUrl { get; private set; }

        public UserDetailsResult(string userName, string displayName, string realName, string email, string avatarUrl, bool canEdit, bool canChangePassword, Url editUrl, Url changePasswordUrl)
        {
            UserName = userName;
            DisplayName = displayName;
            RealName = realName;
            Email = email;
            AvatarUrl = avatarUrl;
            CanEdit = canEdit;
            CanChangePassword = canChangePassword;
            EditUrl = editUrl;
            ChangePasswordUrl = changePasswordUrl;
        }
    }
}