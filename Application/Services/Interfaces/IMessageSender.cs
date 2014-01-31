namespace Application.Services{

	public interface IMessageSender{

		void Send(string to, string subject, string body);

	}

}