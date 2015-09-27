using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Common.Urls.SiteUrls;

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

	    public CashgameListTableModel(CashgameList.Result result)
	    {
            var sortUrl = string.Concat(new CashgameListUrl(result.Slug, result.Year).Relative, "?orderby={0}");

            ListItemModels = GetListItemModels(result);
            DateSortClass = GetSortCssClass(result.SortOrder, CashgameList.SortOrder.Date);
            DateSortUrl = string.Format(sortUrl, CashgameList.SortOrder.Date);
            PlayerSortClass = GetSortCssClass(result.SortOrder, CashgameList.SortOrder.PlayerCount);
            PlayerSortUrl = string.Format(sortUrl, CashgameList.SortOrder.PlayerCount);
            LocationSortClass = GetSortCssClass(result.SortOrder, CashgameList.SortOrder.Location);
            LocationSortUrl = string.Format(sortUrl, CashgameList.SortOrder.Location);
            DurationSortClass = GetSortCssClass(result.SortOrder, CashgameList.SortOrder.Duration);
            DurationSortUrl = string.Format(sortUrl, CashgameList.SortOrder.Duration);
            TurnoverSortClass = GetSortCssClass(result.SortOrder, CashgameList.SortOrder.Turnover);
            TurnoverSortUrl = string.Format(sortUrl, CashgameList.SortOrder.Turnover);
            AverageBuyinSortClass = GetSortCssClass(result.SortOrder, CashgameList.SortOrder.AverageBuyin);
	        AverageBuyinSortUrl = string.Format(sortUrl, CashgameList.SortOrder.AverageBuyin);
	    }

        private List<CashgameListTableItemModel> GetListItemModels(CashgameList.Result result)
        {
            return result.List.Select(o => new CashgameListTableItemModel(o, result.SortOrder, result.SpansMultipleYears)).ToList();
        }

        private string GetSortCssClass(CashgameList.SortOrder selectedSortOrder, CashgameList.SortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "table-list--sortable__sort-column" : "";
        }
	}
}