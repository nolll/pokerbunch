using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;

namespace Web.Models.NavigationModels
{
    public class CashgameYearNavigationModel : Component
    {
        public List<NavigationYearModel> YearModels { get; private set; }
        public string SelectedYear { get; private set; }

        public CashgameYearNavigationModel(CashgameContext.Result cashgameContextResult)
        {
            YearModels = cashgameContextResult.YearItems.Select(o => new NavigationYearModel(o)).ToList();
            SelectedYear = cashgameContextResult.SelectedYear.HasValue ? cashgameContextResult.SelectedYear.ToString() : cashgameContextResult.YearItems.Last().Label;
        }

        public override string ViewName
        {
            get { return "~/Views/Navigation/CashgameYearNavigation.cshtml"; }
        }
    }
}
