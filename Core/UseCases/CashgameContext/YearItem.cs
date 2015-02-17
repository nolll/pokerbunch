using Core.Urls;

namespace Core.UseCases.CashgameContext
{
    public class YearItem
    {
        public string Label { get; private set; }
        public Url Url { get; private set; }
        public bool IsSelected { get; private set; }

        public YearItem(string label, Url url, bool isSelected)
        {
            Label = label;
            Url = url;
            IsSelected = isSelected;
        }
    }
}