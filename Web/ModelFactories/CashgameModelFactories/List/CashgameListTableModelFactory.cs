using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Web.Models.CashgameModels.List;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListTableModelFactory : ICashgameListTableModelFactory
    {
        private readonly ICashgameListTableItemModelFactory _cashgameListTableItemModelFactory;

        public CashgameListTableModelFactory(
            ICashgameListTableItemModelFactory cashgameListTableItemModelFactory)
        {
            _cashgameListTableItemModelFactory = cashgameListTableItemModelFactory;
        }

        public CashgameListTableModel Create(Homegame homegame, IList<Cashgame> cashgames, ListSortOrder sortOrder, int? year)
        {
            var showYear = SpansMultipleYears(cashgames);
            var sortUrl = string.Concat(new CashgameListUrl(homegame.Slug, year).Relative, "?orderby={0}");
            var sortedCashgames = SortCashgames(cashgames, sortOrder);

            return new CashgameListTableModel
                {
                    ShowYear = showYear,
                    ListItemModels = GetListItemModels(homegame, sortedCashgames, showYear, sortOrder),
                    DateSortClass = GetSortCssClass(sortOrder, ListSortOrder.date),
                    DateSortUrl = string.Format(sortUrl, ListSortOrder.date),
                    PlayerSortClass = GetSortCssClass(sortOrder, ListSortOrder.playercount),
                    PlayerSortUrl = string.Format(sortUrl, ListSortOrder.playercount),
                    LocationSortClass = GetSortCssClass(sortOrder, ListSortOrder.location),
                    LocationSortUrl = string.Format(sortUrl, ListSortOrder.location),
                    DurationSortClass = GetSortCssClass(sortOrder, ListSortOrder.duration),
                    DurationSortUrl = string.Format(sortUrl, ListSortOrder.duration),
                    TurnoverSortClass = GetSortCssClass(sortOrder, ListSortOrder.turnover),
                    TurnoverSortUrl = string.Format(sortUrl, ListSortOrder.turnover),
                    AverageBuyinSortClass = GetSortCssClass(sortOrder, ListSortOrder.averagebuyin),
                    AverageBuyinSortUrl = string.Format(sortUrl, ListSortOrder.averagebuyin)
                };
        }

        private List<CashgameListTableItemModel> GetListItemModels(Homegame homegame, IEnumerable<Cashgame> cashgames, bool showYear, ListSortOrder sortOrder)
        {
            return cashgames.Select(cashgame => _cashgameListTableItemModelFactory.Create(homegame, cashgame, showYear, sortOrder)).ToList();
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
                case ListSortOrder.playercount:
                    return cashgames.OrderByDescending(o => o.PlayerCount).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.location:
                    return cashgames.OrderBy(o => o.Location).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.duration:
                    return cashgames.OrderByDescending(o => o.Duration).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.turnover:
                    return cashgames.OrderByDescending(o => o.Turnover).ThenByDescending(o => o.StartTime).ToList();
                case ListSortOrder.averagebuyin:
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

    public enum ListSortOrder
    {
        // ReSharper disable InconsistentNaming
        date,
        playercount,
        location,
        duration,
        turnover,
        averagebuyin
        // ReSharper restore InconsistentNaming
    }

}