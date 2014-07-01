namespace Application.UseCases.CashgameOptions
{
    public interface ICashgameOptionsInteractor
    {
        CashgameOptionsResult Execute(CashgameOptionsRequest request);
    }
}