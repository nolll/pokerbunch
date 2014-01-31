using Core.Classes;

namespace Application.Services
{
    public interface IInvitationMessageBuilder
    {
        string GetSubject(Homegame homegame);
        string GetBody(Homegame homegame, Player player);
    }
}