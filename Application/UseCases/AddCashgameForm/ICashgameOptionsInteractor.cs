namespace Application.UseCases.AddCashgameForm
{
    public interface ICashgameOptionsInteractor
    {
        CashgameOptionsResult Execute(CashgameOptionsRequest request);
    }
}