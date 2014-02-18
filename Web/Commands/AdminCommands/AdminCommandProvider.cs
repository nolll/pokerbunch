using Application.Services;

namespace Web.Commands.AdminCommands
{
    public class AdminCommandProvider : IAdminCommandProvider
    {
        private readonly IMessageSender _messageSender;

        public AdminCommandProvider(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public EmailTestCommand GetEmailTestCommand(string to)
        {
            return new EmailTestCommand(_messageSender, to);
        }
    }
}