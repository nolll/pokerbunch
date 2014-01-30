using Core.Classes;

namespace Application.Services.Interfaces
{
    public interface IInvitationMessageBuilder
    {
        string GetSubject(Homegame homegame);
        string GetBody(Homegame homegame, Player player);
    }
}