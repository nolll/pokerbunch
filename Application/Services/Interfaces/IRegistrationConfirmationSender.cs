using Core.Entities;

namespace Application.Services{

	public interface IRegistrationConfirmationSender{

		void Send(User user, string password);

	}

}