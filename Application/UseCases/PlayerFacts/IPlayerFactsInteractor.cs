namespace Application.UseCases.PlayerFacts
{
    public interface IPlayerFactsInteractor
    {
        PlayerFactsResult Execute(PlayerFactsRequest request);
    }
}