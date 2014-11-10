using Core.Urls;

namespace Core.UseCases.CashgameContext
{
    public class YearItem
    {
        public string Label { get; private set; }
        public Url Url { get; private set; }

        public YearItem(string label, Url url)
        {
            Label = label;
            Url = url;
        }
    }
}