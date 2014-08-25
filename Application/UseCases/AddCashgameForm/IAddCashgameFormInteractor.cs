namespace Application.UseCases.AddCashgameForm
{
    public interface IAddCashgameFormInteractor
    {
        AddCashgameFormResult Execute(AddCashgameFormRequest request);
    }
}