namespace Application.UseCases.TestEmail
{
    public class TestEmailResult
    {
        public string Email { get; private set; }

        public TestEmailResult(string email)
        {
            Email = email;
        }
    }
}