using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Application.UseCases.CashgameList;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListTableModelFactory
    {
        private readonly CashgameListTableItemModelFactory _cashgameListTableItemModelFactory;

        public CashgameListTableModelFactory()
        {
            _cashgameListTableItemModelFactory = new CashgameListTableItemModelFactory();
        }

        public CashgameListTableModel Create(CashgameListResult result)
        {
            var sortUrl = string.Concat(new CashgameListUrl(result.Slug, result.Year).Relative, "?orderby={0}");

            return new CashgameListTableModel
                {
                    ShowYear = result.ShowYear,
                    ListItemModels = GetListItemModels(result),
                    DateSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Date),
                    DateSortUrl = string.Format(sortUrl, ListSortOrder.Date),
                    PlayerSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.PlayerCount),
                    PlayerSortUrl = string.Format(sortUrl, ListSortOrder.PlayerCount),
                    LocationSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Location),
                    LocationSortUrl = string.Format(sortUrl, ListSortOrder.Location),
                    DurationSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Duration),
                    DurationSortUrl = string.Format(sortUrl, ListSortOrder.Duration),
                    TurnoverSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.Turnover),
                    TurnoverSortUrl = string.Format(sortUrl, ListSortOrder.Turnover),
                    AverageBuyinSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.AverageBuyin),
                    AverageBuyinSortUrl = string.Format(sortUrl, ListSortOrder.AverageBuyin)
                };
        }

        private List<CashgameListTableItemModel> GetListItemModels(CashgameListResult result)
        {
            return result.List.Select(o => _cashgameListTableItemModelFactory.Create(o, result.SortOrder, result.SpansMultipleYears)).ToList();
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}