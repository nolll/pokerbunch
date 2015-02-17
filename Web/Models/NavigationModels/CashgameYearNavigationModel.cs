using System.Collections.Generic;
using System.Linq;
using Core.UseCases.CashgameContext;

namespace Web.Models.NavigationModels
{
    public class CashgameYearNavigationModel
    {
        public List<NavigationYearModel> YearModels { get; private set; }

        public CashgameYearNavigationModel(CashgameContextResult cashgameContextResult)
        {
            var year = cashgameContextResult.SelectedYear;
            YearModels = cashgameContextResult.YearItems.Select(o => new NavigationYearModel(o)).ToList();
        }
    }
}
