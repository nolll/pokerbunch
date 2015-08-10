using Core.Services;

namespace Core.UseCases
{
    public class TestEmail
    {
        private readonly IMessageSender _messageSender;

        public TestEmail(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public Result Execute()
        {
            const string email = "henriks@gmail.com";
            var message = new TestMessage();
            _messageSender.Send(email, message);

            return new Result(email);
        }

        public class Result
        {
            public string Email { get; private set; }

            public Result(string email)
            {
                Email = email;
            }
        }

        private class TestMessage : IMessage
        {
            public string Subject
            {
                get { return "Test Email"; }
            }

            public string Body
            {
                get { return "This is a test email from pokerbunch.com"; }
            }
        }
    }
}