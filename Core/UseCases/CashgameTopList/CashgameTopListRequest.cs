namespace Core.UseCases.CashgameTopList
{
    public class CashgameTopListRequest
    {
        public string Slug;
        public ToplistSortOrder OrderBy;
        public int? Year;
    }
}