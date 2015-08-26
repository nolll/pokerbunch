using Core.UseCases;
using Web.Urls;

namespace Web.Models.CashgameModels.Toplist
{
    public class TopListTableColumn
    {
        public string Text { get; private set; }
        public string Url { get; private set; }
        public string CssClass { get; private set; }
        public bool SortingEnabled { get; private set; }

        public TopListTableColumn(TopList.Result topListResult, TopList.SortOrder sortOrder, string text)
        {
            Url = GetSortUrl(topListResult, sortOrder);
            CssClass = GetSortCssClass(topListResult.OrderBy, sortOrder);
            Text = text;
            SortingEnabled = topListResult.OrderBy != TopList.SortOrder.Disabled;
        }

        private string GetSortCssClass(TopList.SortOrder selectedSortOrder, TopList.SortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? ".table-list--sortable__sort-column" : "";
        }

        private string GetSortUrl(TopList.Result topListResult, TopList.SortOrder sortOrder)
        {
            var format = string.Concat(new TopListUrl(topListResult.Slug, topListResult.Year), "?orderby={0}");
            return string.Format(format, GetSortOrderUrlName(sortOrder));
        }

        private string GetSortOrderUrlName(TopList.SortOrder sortOrder)
        {
            return sortOrder.ToString().ToLower();
        }
    }
}