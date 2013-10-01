using Core.Classes;

namespace Core.Services{

	public interface IPasswordSender{

		void Send(User user, string password);

	}

}