namespace Application.UseCases.PlayerDetails
{
    public interface IPlayerDetailsInteractor
    {
        PlayerDetailsResult Execute(PlayerDetailsRequest request);
    }
}