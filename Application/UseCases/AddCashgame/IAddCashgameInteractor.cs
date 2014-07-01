namespace Application.UseCases.AddCashgame
{
    public interface IAddCashgameInteractor
    {
        AddCashgameResult Execute(AddCashgameRequest request);
    }
}