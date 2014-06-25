namespace Application.UseCases.CashgameOptions
{
    public interface IAddCashgameFormInteractor
    {
        AddCashgameFormResult Execute(AddCashgameFormRequest request);
    }
}