namespace Web.Security
{
    public class FakeUserIdentity : IUserIdentity
    {
        public string UserName => "user1";
        public string ApiToken => "token";
        public bool IsAuthenticated => true;
    }
}