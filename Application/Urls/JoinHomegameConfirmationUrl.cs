namespace Application.Urls
{
    public class JoinHomegameConfirmationUrl : HomegameUrl
    {
        public JoinHomegameConfirmationUrl(string slug)
            : base(RouteFormats.HomegameJoinConfirmation, slug)
        {
        }
    }
}