using Core.Entities;

namespace Application.Services
{
    public class InvitationCodeCreator : IInvitationCodeCreator
    {
        private const string Salt = "0lsns5kjdl";

        public string GetCode(Player player)
        {
            return EncryptionService.Encrypt(player.DisplayName, Salt);
        }
    }
}