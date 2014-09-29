namespace Application.UseCases.TestEmail
{
    public class TestMessage : IMessage
    {
        public string Subject
        {
            get { return "Test Email";  }
        }

        public string Body
        {
            get { return "This is a test email from pokerbunch.com"; }
        }
    }
}