using Core.Urls;
using Core.UseCases.CashgameTopList;

namespace Web.Models.CashgameModels.Toplist
{
    public class TopListTableColumn
    {
        public string Text { get; private set; }
        public string Url { get; private set; }
        public string CssClass { get; private set; }
        public bool SortingEnabled { get; private set; }

        public TopListTableColumn(TopListResult topListResult, ToplistSortOrder sortOrder, string text)
        {
            Url = GetSortUrl(topListResult, sortOrder);
            CssClass = GetSortCssClass(topListResult.OrderBy, sortOrder);
            Text = text;
            SortingEnabled = topListResult.OrderBy != ToplistSortOrder.Disabled;
        }

        private string GetSortCssClass(ToplistSortOrder selectedSortOrder, ToplistSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? ".table-list--sortable__sort-column" : "";
        }

        private string GetSortUrl(TopListResult topListResult, ToplistSortOrder sortOrder)
        {
            var format = string.Concat(new TopListUrl(topListResult.Slug, topListResult.Year), "?orderby={0}");
            return string.Format(format, GetSortOrderUrlName(sortOrder));
        }

        private string GetSortOrderUrlName(ToplistSortOrder sortOrder)
        {
            return sortOrder.ToString().ToLower();
        }
    }
}