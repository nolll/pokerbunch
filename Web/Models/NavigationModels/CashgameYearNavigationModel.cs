using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.UseCases.CashgameContext;

namespace Web.Models.NavigationModels
{
    public class CashgameYearNavigationModel
    {
        public string Selected { get; private set; }
        public List<NavigationYearModel> YearModels { get; private set; }

        public CashgameYearNavigationModel(CashgameContextResult cashgameContextResult)
        {
            var year = cashgameContextResult.SelectedYear;

            Selected = year.HasValue ? year.Value.ToString(CultureInfo.InvariantCulture) : "All Time";
            YearModels = cashgameContextResult.YearItems.Select(o => new NavigationYearModel(o)).ToList();
        }
    }
}
