namespace Core.Urls
{
    public class DeleteCashgameUrl : Url
    {
        public DeleteCashgameUrl(string slug, int id)
            : base(BuildUrl(slug, id))
        {
        }

        private static string BuildUrl(string slug, int id)
        {
            var url = RouteParams.ReplaceSlug(Routes.CashgameDelete, slug);
            return RouteParams.ReplaceId(url, id);
        }
    }
}