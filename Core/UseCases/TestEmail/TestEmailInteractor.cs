using Core.Services;

namespace Core.UseCases.TestEmail
{
    public class TestEmailInteractor
    {
        private readonly IMessageSender _messageSender;

        public TestEmailInteractor(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public TestEmailResult Execute()
        {
            const string email = "henriks@gmail.com";
            var message = new TestMessage();
            _messageSender.Send(email, message);

            return new TestEmailResult(email);
        }
    }
}