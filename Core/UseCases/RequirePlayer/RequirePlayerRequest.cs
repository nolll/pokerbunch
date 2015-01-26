namespace Core.UseCases.RequirePlayer
{
    public class RequirePlayerRequest
    {
        public string Slug { get; private set; }
        public string UserName { get; private set; }

        public RequirePlayerRequest(string slug, string userName)
        {
            Slug = slug;
            UserName = userName;
        }
    }
}