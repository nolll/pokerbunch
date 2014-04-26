namespace Application.UseCases.CashgameFacts
{
    public interface ICashgameFactsInteractor
    {
        CashgameFactsResult Execute(CashgameFactsRequest request);
    }
}