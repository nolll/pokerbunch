using Core.Classes;

namespace Core.Services{

	public interface IInvitationSender{

		void Send(Homegame homegame, Player player, string email);

	}

}