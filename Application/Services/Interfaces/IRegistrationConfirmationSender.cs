using Core.Classes;

namespace Application.Services{

	public interface IRegistrationConfirmationSender{

		void Send(User user, string password);

	}

}