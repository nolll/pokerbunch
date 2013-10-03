using Core.Classes;

namespace Core.Services{

	public interface IRegistrationConfirmationSender{

		void Send(User user, string password);

	}

}