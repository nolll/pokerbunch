namespace Application.UseCases.CashgameDetails
{
    public interface ICashgameDetailsInteractor
    {
        CashgameDetailsResult Execute(CashgameDetailsRequest request);
    }
}