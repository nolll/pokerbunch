namespace Application.UseCases.PlayerBadges
{
    public interface IPlayerBadgesInteractor
    {
        PlayerBadgesResult Execute(PlayerBadgesRequest request);
    }
}