namespace Core.UseCases.RequireManager
{
    public class RequireManagerRequest
    {
        public string Slug { get; private set; }
        public string UserName { get; private set; }

        public RequireManagerRequest(string slug, string userName)
        {
            Slug = slug;
            UserName = userName;
        }
    }
}