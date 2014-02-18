using Application.Services;

namespace Web.Commands.AdminCommands
{
    public class EmailTestCommand : Command
    {
        private readonly IMessageSender _messageSender;
        private readonly string _email;

        public EmailTestCommand(
            IMessageSender messageSender,
            string email)
        {
            _messageSender = messageSender;
            _email = email;
        }

        public override bool Execute()
        {
            const string subject = "Test Email";
            const string body = "This is a test email from pokerbunch.com";
            _messageSender.Send(_email, subject, body);
            return true;
        }
    }
}