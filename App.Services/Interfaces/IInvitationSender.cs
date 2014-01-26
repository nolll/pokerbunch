using Core.Classes;

namespace App.Services.Interfaces{

	public interface IInvitationSender{

		void Send(Homegame homegame, Player player, string email);

	}

}