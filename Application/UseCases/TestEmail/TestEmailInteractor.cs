using Application.Services;

namespace Application.UseCases.TestEmail
{
    public static class TestEmailInteractor
    {
        public static TestEmailResult Execute(IMessageSender messageSender)
        {
            const string email = "henriks@gmail.com";
            var message = new TestMessage();
            messageSender.Send(email, message);

            return new TestEmailResult(email);
        }
    }
}