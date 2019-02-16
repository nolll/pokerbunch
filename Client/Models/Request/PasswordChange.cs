namespace PokerBunch.Client.Models.Request
{
    public class PasswordChange
    {
        public string OldPassword { get; }
        public string NewPassword { get; }
        public string Repeat { get; }

        public PasswordChange(string oldPassword, string newPassword, string repeat)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            Repeat = repeat;
        }
    }
}