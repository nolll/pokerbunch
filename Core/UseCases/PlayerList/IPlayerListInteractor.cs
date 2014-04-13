namespace Core.UseCases.PlayerList
{
    public interface IPlayerListInteractor
    {
        PlayerListResult Execute(PlayerListRequest request);
    }

    public class PlayerListRequest
    {
        public string Slug;
    }
}