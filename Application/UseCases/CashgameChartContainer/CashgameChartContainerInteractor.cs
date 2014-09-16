using Application.Urls;

namespace Application.UseCases.CashgameChartContainer
{
    public static class CashgameChartContainerInteractor
    {
        public static CashgameChartContainerResult Execute(CashgameChartContainerRequest request)
        {
            var dataUrl = new CashgameChartJsonUrl(request.Slug, request.Year);

            return new CashgameChartContainerResult(dataUrl);
        }
    }
}