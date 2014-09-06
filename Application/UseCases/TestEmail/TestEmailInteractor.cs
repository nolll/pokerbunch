using Application.Services;

namespace Application.UseCases.TestEmail
{
    public static class TestEmailInteractor
    {
        public static TestEmailResult Execute(IMessageSender messageSender)
        {
            const string email = "henriks@gmail.com";
            const string subject = "Test Email";
            const string body = "This is a test email from pokerbunch.com";
            messageSender.Send(email, subject, body);

            return new TestEmailResult(email);
        }
    }
}