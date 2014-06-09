using Application.UseCases.CashgameTopList;
using Web.Services;

namespace Web.Models.CashgameModels.Toplist
{
    public class TopListTableColumn
    {
        public string Text { get; private set; }
        public string Url { get; private set; }
        public string CssClass { get; private set; }

        public TopListTableColumn(TopListResult topListResult, ToplistSortOrder sortOrder, string text)
        {
            Url = GetSortUrl(topListResult, sortOrder);
            CssClass = GetSortCssClass(topListResult.OrderBy, sortOrder);
            Text = text;
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }

        private string GetSortUrl(TopListResult topListResult, ToplistSortOrder sortOrder)
        {
            var format = string.Concat(UrlProvider.GetCashgameToplistUrlStatic(topListResult.Slug, topListResult.Year), "?orderby={0}");
            return string.Format(format, GetSortOrderUrlName(sortOrder));
        }

        private string GetSortOrderUrlName(ToplistSortOrder sortOrder)
        {
            return sortOrder.ToString().ToLower();
        }
    }
}