using Core.Classes;

namespace Application.Services{

	public interface IInvitationSender{

		void Send(Homegame homegame, Player player, string email);

	}

}