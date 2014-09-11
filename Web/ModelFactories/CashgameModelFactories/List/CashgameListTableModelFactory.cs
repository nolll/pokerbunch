using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Application.UseCases.CashgameList;
using Core.Entities;
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
 //           var showYear = SpansMultipleYears(cashgames);
            var sortUrl = string.Concat(new CashgameListUrl(result.Slug, result.Year).Relative, "?orderby={0}");

            return new CashgameListTableModel
                {
                    ShowYear = result.ShowYear,
                    ListItemModels = GetListItemModels(result),
                    DateSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.date),
                    DateSortUrl = string.Format(sortUrl, ListSortOrder.date),
                    PlayerSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.playercount),
                    PlayerSortUrl = string.Format(sortUrl, ListSortOrder.playercount),
                    LocationSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.location),
                    LocationSortUrl = string.Format(sortUrl, ListSortOrder.location),
                    DurationSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.duration),
                    DurationSortUrl = string.Format(sortUrl, ListSortOrder.duration),
                    TurnoverSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.turnover),
                    TurnoverSortUrl = string.Format(sortUrl, ListSortOrder.turnover),
                    AverageBuyinSortClass = GetSortCssClass(result.SortOrder, ListSortOrder.averagebuyin),
                    AverageBuyinSortUrl = string.Format(sortUrl, ListSortOrder.averagebuyin)
                };
        }

        private List<CashgameListTableItemModel> GetListItemModels(CashgameListResult result)
        {
            return result.List.Select(o => _cashgameListTableItemModelFactory.Create(o, result.SortOrder)).ToList();
        }

        private bool SpansMultipleYears(IEnumerable<Cashgame> cashgames)
        {
            var years = new List<int>();
            foreach (var cashgame in cashgames)
            {
                if (cashgame.StartTime.HasValue)
                {
                    var year = cashgame.StartTime.Value.Year;
                    if (!years.Contains(year))
                    {
                        years.Add(year);
                    }
                }
            }
            return years.Count > 1;
        }

        private IEnumerable<Cashgame> SortCashgames(IEnumerable<Cashgame> cashgames, ListSortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case ListSortOrder.PlayerCount:
                    return cashgames.OrderByDescending(o => o.PlayerCount).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.Location:
                    return cashgames.OrderBy(o => o.Location).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.Duration:
                    return cashgames.OrderByDescending(o => o.Duration).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.Turnover:
                    return cashgames.OrderByDescending(o => o.Turnover).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.AverageBuyin:
                    return cashgames.OrderByDescending(o => o.AverageBuyin).ThenByDescending(o => o.StartTime).ToList();
                default:
                    return cashgames.OrderByDescending(o => o.StartTime).ToList();
            }
        }

        private string GetSortCssClass(ListSortOrder selectedSortOrder, ListSortOrder columnSortOrder)
        {
            return selectedSortOrder.Equals(columnSortOrder) ? "sort-column" : "";
        }
    }
}