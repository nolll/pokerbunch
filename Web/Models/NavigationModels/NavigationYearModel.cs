using Core.UseCases.CashgameContext;

namespace Web.Models.NavigationModels
{
    public class NavigationYearModel
    {
        public string Text { get; private set; }
        public string Url { get; private set; }

        public NavigationYearModel(YearItem yearItem)
        {
            Text = yearItem.Label;
            Url = yearItem.Url.Relative;
        }
    }
}
