namespace Core.UseCases.RequireManager
{
    public class RequireAdminRequest
    {
        public string UserName { get; private set; }

        public RequireAdminRequest(string userName)
        {
            UserName = userName;
        }
    }
}