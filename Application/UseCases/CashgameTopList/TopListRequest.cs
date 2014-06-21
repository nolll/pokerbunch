namespace Application.UseCases.CashgameTopList
{
    public class TopListRequest
    {
        public string Slug;
        public ToplistSortOrder OrderBy;
        public int? Year;

        public TopListRequest(string slug, ToplistSortOrder orderBy, int? year)
        {
            Slug = slug;
            OrderBy = orderBy;
            Year = year;
        }
    }
}