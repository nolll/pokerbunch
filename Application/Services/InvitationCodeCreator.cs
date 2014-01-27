using Application.Services.Interfaces;
using Core.Classes;

namespace Application.Services
{
	public class InvitationCodeCreator : IInvitationCodeCreator
    {
	    private const string Salt = "0lsns5kjdl";
	    private readonly IEncryptionService _encryptionService;

		public InvitationCodeCreator(IEncryptionService encryptionService){
			_encryptionService = encryptionService;
		}

		public string GetCode(Player player){
			return _encryptionService.Encrypt(player.DisplayName, Salt);
		}

	}

}