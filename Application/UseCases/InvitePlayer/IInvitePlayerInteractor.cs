namespace Application.UseCases.InvitePlayer
{
    public interface IInvitePlayerInteractor
    {
        InvitePlayerResult Execute(InvitePlayerRequest request);
    }
}