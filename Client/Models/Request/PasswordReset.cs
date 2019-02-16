namespace PokerBunch.Client.Models.Request
{
    public class PasswordReset
    {
        public string Email { get; }

        public PasswordReset(string email)
        {
            Email = email;
        }
    }
}