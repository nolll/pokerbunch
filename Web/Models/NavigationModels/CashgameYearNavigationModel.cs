using System.Collections.Generic;
using System.Linq;
using Core.UseCases.CashgameContext;
using Web.Extensions;

namespace Web.Models.NavigationModels
{
    public class CashgameYearNavigationModel : IViewModel
    {
        public List<NavigationYearModel> YearModels { get; private set; }
        public string SelectedYear { get; private set; }

        public CashgameYearNavigationModel(CashgameContextResult cashgameContextResult)
        {
            YearModels = cashgameContextResult.YearItems.Select(o => new NavigationYearModel(o)).ToList();
            SelectedYear = cashgameContextResult.SelectedYear.HasValue ? cashgameContextResult.SelectedYear.ToString() : cashgameContextResult.YearItems.Last().Label;
        }

        public string ViewName
        {
            get { return "~/Views/Navigation/CashgameYearNavigation.cshtml"; }
        }
    }
}
