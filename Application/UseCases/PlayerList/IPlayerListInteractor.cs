namespace Application.UseCases.PlayerList
{
    public interface IPlayerListInteractor
    {
        PlayerListResult Execute(PlayerListRequest request);
    }
}