namespace App.Services.Interfaces{

	public interface IMessageSender{

		void Send(string to, string subject, string body);

	}

}