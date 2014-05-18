using Core.Entities;

namespace Application.Services{

	public interface IPasswordSender{

		void Send(User user, string password);

	}

}