namespace Application.UseCases.CashgameTopList
{
    public interface ICashgameTopListInteractor
    {
        CashgameTopListResult Execute(CashgameTopListRequest request);
    }
}