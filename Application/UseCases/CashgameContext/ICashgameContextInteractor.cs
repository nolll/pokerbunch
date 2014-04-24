namespace Application.UseCases.CashgameContext
{
    public interface ICashgameContextInteractor
    {
        CashgameContextResult Execute(CashgameContextRequest request);
    }
}