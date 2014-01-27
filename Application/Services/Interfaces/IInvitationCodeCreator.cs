using Core.Classes;

namespace App.Services.Interfaces{
    public interface IInvitationCodeCreator{

		string GetCode(Player player);

	}

}