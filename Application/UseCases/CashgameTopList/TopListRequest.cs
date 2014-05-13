namespace Application.UseCases.CashgameTopList
{
    public class TopListRequest
    {
        public string Slug;
        public ToplistSortOrder OrderBy;
        public int? Year;
    }
}