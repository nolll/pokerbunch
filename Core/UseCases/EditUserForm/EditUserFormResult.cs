namespace Core.UseCases.EditUserForm
{
    public class EditUserFormResult
    {
        public string UserName { get; private set; }
        public string RealName { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        public EditUserFormResult(string userName, string realName, string displayName, string email)
        {
            UserName = userName;
            RealName = realName;
            DisplayName = displayName;
            Email = email;
        }
    }
}