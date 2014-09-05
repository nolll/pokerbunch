using Application.Services;

namespace Application.UseCases.TestEmail
{
    public class TestEmailInteractor : ITestEmailInteractor
    {
        private readonly IMessageSender _messageSender;

        public TestEmailInteractor(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public TestEmailResult Execute()
        {
            const string email = "henriks@gmail.com";
            const string subject = "Test Email";
            const string body = "This is a test email from pokerbunch.com";
            _messageSender.Send(email, subject, body);

            return new TestEmailResult(email);
        }
    }
}