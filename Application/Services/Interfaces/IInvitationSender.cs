using Core.Entities;

namespace Application.Services{

	public interface IInvitationSender{

		void Send(Bunch bunch, Player player, string email);

	}

}