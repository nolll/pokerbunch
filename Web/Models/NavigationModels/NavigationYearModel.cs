using Core.UseCases;

namespace Web.Models.NavigationModels
{
    public class NavigationYearModel
    {
        public string Text { get; private set; }
        public string Url { get; private set; }
        public string SelectedCssClass { get; private set; }

        public NavigationYearModel(CashgameContext.YearItem yearItem)
        {
            Text = yearItem.Label;
            Url = yearItem.Url.Relative;
            SelectedCssClass = yearItem.IsSelected ? "selected" : null;
        }
    }
}
