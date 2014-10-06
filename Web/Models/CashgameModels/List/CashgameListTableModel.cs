using System.Collections.Generic;
using System.Linq;
using Core.Urls;
using Core.UseCases.CashgameList;

namespace Web.Models.CashgameModels.List{

	public class CashgameListTableModel
    {
        public List<CashgameListTableItemModel> ListItemModels { get; private set; }
        public object DateSortClass { get; private set; }
        public object DateSortUrl { get; private set; }
        public object PlayerSortClass { get; private set; }
        public object PlayerSortUrl { get; private set; }
        public object LocationSortClass { get; private set; }
        public object LocationSortUrl { get; private set; }
        public object DurationSortClass { get; private set; }
        public object DurationSortUrl { get; private set; }
        public object TurnoverSortClass { get; private set; }
        public object TurnoverSortUrl { get; private set; }
        public object AverageBuyinSortClass { get; private set; }
        public object AverageBuyinSortUrl { get; private set; }

	    public CashgameListTableModel(CashgameListResult result)
	    {
            var sortUrl = string.Concat(new CashgameListUrl(result.Slug, result.Year).Relative, "?orderby={0}");

            ListItemModels = GetListItemModels(result);
            DateSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Date);
            DateSortUrl = string.Format(sortUrl, ListSortOrder.Date);
            PlayerSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.PlayerCount);
            PlayerSortUrl = string.Format(sortUrl, ListSortOrder.PlayerCount);
            LocationSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Location);
            LocationSortUrl = string.Format(sortUrl, ListSortOrder.Location);
            DurationSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Duration);
            DurationSortUrl = string.Format(sortUrl, ListSortOrder.Duration);
            TurnoverSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Turnover);
            TurnoverSortUrl = string.Format(sortUrl, ListSortOrder.Turnover);
            AverageBuyinSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.AverageBuyin);
	        AverageBuyinSortUrl = string.Format(sortUrl, ListSortOrder.AverageBuyin);
	    }

        private List<CashgameListTableItemModel> GetListItemModels(CashgameListResult result)
        {
            return result.List.Select(o => new CashgameListTableItemModel(o, result.SortOrder, result.SpansMultipleYears)).ToList();
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
	}
}