namespace Application.UseCases.PlayerList
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