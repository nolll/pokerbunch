namespace Web.Security
{
    public interface IUserIdentity
    {
        string UserName { get; }
        string ApiToken { get; }
        bool IsAuthenticated { get; }
    }
}