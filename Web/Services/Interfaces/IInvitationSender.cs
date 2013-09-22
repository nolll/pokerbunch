using Core.Classes;

namespace Web.Services.Interfaces{

	public interface IInvitationSender{

		void Send(Homegame homegame, Player player, string email);

	}

}