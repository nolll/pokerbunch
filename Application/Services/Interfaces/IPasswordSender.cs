using Core.Classes;

namespace App.Services.Interfaces{

	public interface IPasswordSender{

		void Send(User user, string password);

	}

}