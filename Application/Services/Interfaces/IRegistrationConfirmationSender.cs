using Core.Classes;

namespace Application.Services.Interfaces{

	public interface IRegistrationConfirmationSender{

		void Send(User user, string password);

	}

}