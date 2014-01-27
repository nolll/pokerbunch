using Core.Classes;

namespace Application.Services.Interfaces{

	public interface IInvitationSender{

		void Send(Homegame homegame, Player player, string email);

	}

}