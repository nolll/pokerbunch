using Core.Entities;

namespace Application.Services{

	public interface IInvitationSender{

		void Send(Homegame homegame, Player player, string email);

	}

}