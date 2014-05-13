namespace Application.UseCases.CashgameTopList
{
    public interface ITopListInteractor
    {
        TopListResult Execute(TopListRequest request);
    }
}