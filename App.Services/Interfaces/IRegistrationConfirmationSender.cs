using Core.Classes;

namespace App.Services.Interfaces{

	public interface IRegistrationConfirmationSender{

		void Send(User user, string password);

	}

}