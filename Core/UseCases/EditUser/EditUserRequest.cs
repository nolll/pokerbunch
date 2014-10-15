namespace Core.UseCases.EditUser
{
    public class EditUserRequest
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }

        public EditUserRequest(string userName, string displayName, string realName, string email)
        {
            UserName = userName;
            DisplayName = displayName;
            RealName = realName;
            Email = email;
        }
    }
}