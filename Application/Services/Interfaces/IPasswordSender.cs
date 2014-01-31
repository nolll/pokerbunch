using Core.Classes;

namespace Application.Services{

	public interface IPasswordSender{

		void Send(User user, string password);

	}

}