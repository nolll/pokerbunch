using Application.Urls;

namespace Application.UseCases.CashgameChartContainer
{
    public class CashgameChartContainerResult
    {
        public Url DataUrl { get; private set; }

        public CashgameChartContainerResult(Url dataUrl)
        {
            DataUrl = dataUrl;
        }
    }
}