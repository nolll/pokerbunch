using Core.Classes;

namespace Application.Services.Interfaces{

	public interface IPasswordSender{

		void Send(User user, string password);

	}

}